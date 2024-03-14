using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gigadrillgames.GameFrameWork.Utils;
using UnityEngine;

public class AudioRenderer
{
    #region Fields, Properties, and Inner Classes

    // constants for the wave file header
    private const int HEADER_SIZE = 44;
    private const short BITS_PER_SAMPLE = 16;
    private const int SAMPLE_RATE = 44100;

    // the number of audio Channels in the output file
    public int Channels = 2;

    // the audio stream instance
    private MemoryStream outputStream;
    private BinaryWriter outputWriter;
    
    /// The status of a render
    public enum Status
    {
        UNKNOWN,
        SUCCESS,
        FAIL,
        ASYNC
    }

    /// The result of a render.
    public class Result
    {
        public Status State;
        public string Message;

        public Result(Status newState = Status.UNKNOWN, string newMessage = "")
        {
            this.State = newState;
            this.Message = newMessage;
        }
    }

    #endregion

    public AudioRenderer()
    {
        this.Clear();
    }

    // reset the renderer
    public void Clear()
    {
        this.outputStream = new MemoryStream();
        this.outputWriter = new BinaryWriter(outputStream);
    }

    /// Write a chunk of data to the output stream.
    public void Write(float[] audioData)
    {
        // Convert numeric audio data to bytes
        for (int i = 0; i < audioData.Length; i++)
        {
            // write the short to the stream
            this.outputWriter.Write((short) (audioData[i] * (float) Int16.MaxValue));
        }
    }

    #region File I/O

    public Result Save(string filename)
    {
        Result result = new Result();

        if (outputStream.Length > 0)
        {
            // add a header to the file so we can send it to the SoundPlayer
            this.AddHeader();

            // if a filename was passed in
            if (filename.Length > 0)
            {
                // Save to a file. Print a warning if overwriting a file.
                if (File.Exists(filename))
                    Debug.LogWarning("Overwriting " + filename + "...");

                // reset the stream pointer to the beginning of the stream
                outputStream.Position = 0;

                // write the stream to a file
                FileStream fs = File.OpenWrite(filename);

                this.outputStream.WriteTo(fs);

                fs.Close();

                // for debugging only
                Debug.Log("Finished saving to " + filename + ".");
            }

            result.State = Status.SUCCESS;
            result.Message = "Finished saving to " + filename + ".";
        }
        else
        {
            Debug.LogWarning("There is no audio data to save!");

            result.State = Status.FAIL;
            result.Message = "There is no audio data to save!";
        }

        return result;
    }

    /// This generates a simple header for a canonical wave file, 
    /// which is the simplest practical audio file format. It
    /// writes the header and the audio file to a new stream, then
    /// moves the reference to that stream.
    /// 
    /// See this page for details on canonical wave files: 
    /// http://www.lightlink.com/tjweber/StripWav/Canon.html
    private void AddHeader()
    {
        // reset the output stream
        outputStream.Position = 0;

        // calculate the number of samples in the data chunk
        long numberOfSamples = outputStream.Length / (BITS_PER_SAMPLE / 8);

        // create a new MemoryStream that will have both the audio data AND the header
        MemoryStream newOutputStream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(newOutputStream);

        writer.Write(0x46464952); // "RIFF" in ASCII

        // write the number of bytes in the entire file
        writer.Write((int) (HEADER_SIZE + (numberOfSamples * BITS_PER_SAMPLE * Channels / 8)) - 8);

        writer.Write(0x45564157); // "WAVE" in ASCII
        writer.Write(0x20746d66); // "fmt " in ASCII
        writer.Write(16);

        // write the format tag. 1 = PCM
        writer.Write((short) 1);

        // write the number of Channels.
        writer.Write((short) Channels);

        // write the sample rate. 44100 in this case. The number of audio samples per second
        writer.Write(SAMPLE_RATE);

        writer.Write(SAMPLE_RATE * Channels * (BITS_PER_SAMPLE / 8));
        writer.Write((short) (Channels * (BITS_PER_SAMPLE / 8)));

        // 16 bits per sample
        writer.Write(BITS_PER_SAMPLE);

        // "data" in ASCII. Start the data chunk.
        writer.Write(0x61746164);

        // write the number of bytes in the data portion
        writer.Write((int) (numberOfSamples * BITS_PER_SAMPLE * Channels / 8));

        // copy over the actual audio data
        this.outputStream.WriteTo(newOutputStream);

        // move the reference to the new stream
        this.outputStream = newOutputStream;
    }

    private float ClampToValidRange(float value)
    {
        float min = -1.0f;
        float max = 1.0f;
        return (value < min) ? min : (value > max) ? max : value;
    }

    private float[] MixAndClampFloatBuffers(float[] bufferA, float[] bufferB)
    {
        int maxLength = Math.Min(bufferA.Length, bufferB.Length);
        float[] mixedFloatArray = new float[maxLength];

        // merged same time line
        for (int i = 0; i < maxLength; i++)
        {
            if ( i < bufferA.Length && i < bufferB.Length)
            {
                mixedFloatArray[i] = ClampToValidRange((bufferA[i] + bufferB[i])/2);    
            }
            else
            {
                if ( i < bufferA.Length)
                {
                    mixedFloatArray[i] = ClampToValidRange(bufferA[i]);    
                }else if ( i < bufferB.Length)
                {
                    mixedFloatArray[i] = ClampToValidRange(bufferB[i]);    
                }   
            }
        }

        // add to end
        var comb = bufferA.Concat(bufferB).ToList();

        for (int i = 0; i < comb.Count; i++)
        {
            mixedFloatArray[i] = comb[i];
        }

        return mixedFloatArray;
    }
    
    private float[] StitchAudioData(float[] bufferA, float[] bufferB)
    {
        int maxLength = Math.Min(bufferA.Length, bufferB.Length);
        float[] mixedFloatArray = new float[maxLength];

        // add to end
        var comb = bufferA.Concat(bufferB).ToList();

        for (int i = 0; i < comb.Count; i++)
        {
            mixedFloatArray[i] = comb[i];
        }

        return mixedFloatArray;
    }
    #endregion
}