const MicrophonePlugin = {
    $state: {
        availableDevices: [],
        active: /* {
             audioContext,
        } */ undefined,
        lastSamples: /* [] */ undefined,
        callbacks: /* {
            permissionGranted,
            permissionDenied,
            deviceChange,
            trackEnded,
            newSamples,
        } */ undefined,
        settings: {
            useAudioWorklet: undefined,
            minSampleRate: undefined,
            maxSampleRate: undefined,
            supportDynamicSampleRate: undefined,
            targetLatency: undefined,
            targetFrameDuration: undefined,
        },
    },

    $getAppropriateBufferSize: function (sampleRate, frameDuration, maxFrameDuration) {
        const maxBufferSize = maxFrameDuration * sampleRate;

        const availableBufferSizes = [256, 512, 1024, 2048, 4096, 8192, 16384]
            .filter(size => size <= maxBufferSize);

        if (availableBufferSizes.length === 0) {
            throw new Error(`Maximum buffer size (${maxBufferSize} samples, ${maxBufferSize / sampleRate} seconds) is too small`);
        }

        const preferredBufferSize = frameDuration * sampleRate;

        return availableBufferSizes
            .reduce((prev, curr) => Math.abs(curr - preferredBufferSize) < Math.abs(prev - preferredBufferSize) ? curr : prev);
    },

    $createScriptProcessor: async function (audioContext, callback, bufferSize) {
        const inputChannels = 1;
        const outputChannels = 1;

        const node = audioContext.createScriptProcessor(bufferSize, inputChannels, outputChannels);

        node.onaudioprocess = e => {
            callback(e.inputBuffer.getChannelData(0));
        };

        return node;
    },

    $createAudioWorklet: async function (audioContext, callback, bufferSize) {
        const script = `
            class AudioCaptureWorklet extends AudioWorkletProcessor {
                constructor(options) {
                    super();
                    this._buffer = new Float32Array(options.processorOptions.bufferSize);
                    this._written = 0;
                }
            
                process(inputList, outputList, parameters) {
                    const samples = inputList[0]?.[0];
            
                    if (!samples || samples.length === 0) {
                        return true;
                    }
            
                    let availableSamples = samples.length;
                    let read = 0;
            
                    while (availableSamples > 0) {
                        const canWriteBeforeOverflow = this._buffer.length - this._written;
            
                        const willWrite = Math.min(canWriteBeforeOverflow, availableSamples);
            
                        for (let i = read; i < read + willWrite; i++) {
                            this._buffer[this._written++] = samples[i];
                        }
            
                        if (this._written === this._buffer.length) {
                            this._written = 0;
            
                            this.port.postMessage({
                                samples: this._buffer
                            });
                        }
            
                        availableSamples -= willWrite;
                        read += willWrite;
                    }
            
                    return true;
                }
            }
            
            registerProcessor("AudioCaptureWorklet", AudioCaptureWorklet);
        `;

        const blob = new Blob([script], {
            type: "application/javascript"
        });

        const moduleUrl = URL.createObjectURL(blob);
        await audioContext.audioWorklet.addModule(moduleUrl);

        const node = new AudioWorkletNode(audioContext, "AudioCaptureWorklet", {processorOptions: {bufferSize}});
        node.port.onmessage = e => callback(e.data.samples);

        return node;
    },

    $stringToPtr: function (text) {
        const bufferSize = lengthBytesUTF8(text) + 1;
        const buffer = _malloc(bufferSize);
        stringToUTF8(text, buffer, bufferSize);
        return buffer;
    },

    $readAudioDevices: async function () {
        const devices = await navigator.mediaDevices.enumerateDevices()
        return devices.filter(device => device.kind === "audioinput")
    },

    Estrada_Initialize__deps: ["$state"],
    Estrada_Initialize: function (targetLatency, targetFrameDuration) {
        const supportDynamicSampleRate = navigator.mediaDevices.getSupportedConstraints().sampleRate !== undefined;

        const fixedSampleRate = !supportDynamicSampleRate && new AudioContext().sampleRate;

        if (!supportDynamicSampleRate) {
            console.log(`This browser does not support setting custom sample rate. Media will be sampled at ${fixedSampleRate} Hz`)
        }

        state.settings = {
            useAudioWorklet: true,
            targetLatency,
            targetFrameDuration,
            supportDynamicSampleRate,
            // Web Audio API implementation must support sample rates in range 8kHz-96kHz.
            minSampleRate: fixedSampleRate || 8000,
            maxSampleRate: fixedSampleRate || 96000
        };
    },

    Estrada_MinSampleRate__deps: ["$state"],
    Estrada_MinSampleRate: function () {
        return state.settings.minSampleRate;
    },

    Estrada_MaxSampleRate__deps: ["$state"],
    Estrada_MaxSampleRate: function () {
        return state.settings.maxSampleRate;
    },

    Estrada_InstallCallbacks__deps: ["$state", "$stringToPtr"],
    Estrada_InstallCallbacks: function (permissionGrantedPtr, permissionDeniedPtr, deviceChangePtr, trackEndedPtr, newSamplesPtr) {
        state.callbacks = {
            permissionGranted: () => Module.dynCall_v(permissionGrantedPtr),
            permissionDenied: msg => Module.dynCall_vi(permissionDeniedPtr, stringToPtr(msg)),
            deviceChange: () => Module.dynCall_v(deviceChangePtr),
            trackEnded: deviceName => Module.dynCall_vi(trackEndedPtr, stringToPtr(deviceName)),
            newSamples: () => Module.dynCall_v(newSamplesPtr),
        }
    },

    Estrada_RequestPermission__deps: ["$state", "$readAudioDevices"],
    Estrada_RequestPermission: async function () {
        try {
            await navigator.mediaDevices.getUserMedia({
                audio: {}
            });

            const updateDevices = async () => {
                const now = await readAudioDevices();
                const prev = state.availableDevices;

                if (now.length !== prev.length || prev.every((v, i) => v === now[i])) {
                    state.availableDevices = now;
                    state.callbacks.deviceChange();
                }
            }

            await updateDevices();

            navigator.mediaDevices.ondevicechange = updateDevices;

            state.callbacks.permissionGranted();
        } catch (e) {
            console.error(e.message)
            state.callbacks.permissionDenied(e.message);
        }
    },

    Estrada_WriteSamples__deps: ["$state"],
    Estrada_WriteSamples: function (sharedBufferBytesOffset, sharedBufferLength, from, allowOverflow) {
        if (!state.lastSamples) {
            return 0;
        }

        const sharedBuffer = new Float32Array(buffer, sharedBufferBytesOffset, sharedBufferLength);
        const samples = state.lastSamples;

        let availableSamples = samples.length;
        let written = from;
        let read = 0;

        while (availableSamples > 0) {
            const canWriteBeforeOverflow = sharedBuffer.length - written;

            const willWrite = Math.min(canWriteBeforeOverflow, availableSamples);

            for (let i = read; i < read + willWrite; i++) {
                sharedBuffer[written++] = samples[i];
            }

            if (written === sharedBuffer.length) {
                if (!allowOverflow) {
                    return willWrite;
                }

                written = 0;
            }

            availableSamples -= willWrite;
            read += willWrite;
        }

        return samples.length;
    },

    Estrada_Start__deps: ["$state", "$getAppropriateBufferSize", "$createScriptProcessor", "$createAudioWorklet", "$stringToPtr"],
    Estrada_Start: async function (deviceNameUTF8, sampleRate, maxFrameDuration) {
        const deviceName = UTF8ToString(deviceNameUTF8);

        try {
            if (state.active) {
                await state.active.audioContext.close();
            }

            const audioContext = new AudioContext({
                sampleRate : state.settings.supportDynamicSampleRate ? sampleRate : undefined,
                latencyHint: "interactive"
            });

            const samplesPacketLength = getAppropriateBufferSize(sampleRate, state.settings.targetFrameDuration, maxFrameDuration);

            // await audioContext.resume();

            const selectedDevice = state.availableDevices.find(device => device.label === deviceName)

            const stream = await navigator.mediaDevices.getUserMedia({
                audio: {
                    deviceId: selectedDevice.deviceId,
                    channelCount: 1,
                    latency: state.settings.targetLatency,
                    echoCancellation: false,
                    noiseSuppression: false,
                    autoGainControl: false,
                }
            });

            stream.getTracks().forEach(track => track.onended = () => {
                state.callbacks.trackEnded(deviceName);
                state.active.audioContext.close()
                state.active = undefined;
            });

            const micSource = audioContext.createMediaStreamSource(stream);

            const isSecureContext = document.location.protocol.includes('https');

            const shouldUseAudioWorklet = isSecureContext && state.settings.useAudioWorklet;

            const factory = shouldUseAudioWorklet
                ? createAudioWorklet
                : createScriptProcessor;

            console.log(`Create ${shouldUseAudioWorklet ? 'audio worklet' : 'script processor'}`);

            const audioCaptureNode = await factory(audioContext, data => {
                state.lastSamples = data;
                state.callbacks.newSamples();
            }, samplesPacketLength);

            micSource
                .connect(audioCaptureNode)
                .connect(audioContext.destination);

            state.active = {
                audioContext
            };
        } catch (e) {
            console.error(e.message);
            state.callbacks.trackEnded(deviceName);
        }
    },

    Estrada_End__deps: ["$state"],
    Estrada_End: async function () {
        if (!state.active) {
            return;
        }

        await state.active.audioContext.close();

        state.active = undefined;
    },

    Estrada_AvailableDevicesNum__deps: ["$state"],
    Estrada_AvailableDevicesNum: function () {
        return state.availableDevices.length;
    },

    Estrada_DeviceNameAt__deps: ["$state", "$stringToPtr"],
    Estrada_DeviceNameAt: function (index) {
        return stringToPtr(state.availableDevices[index].label);
    },
};

mergeInto(LibraryManager.library, MicrophonePlugin);
