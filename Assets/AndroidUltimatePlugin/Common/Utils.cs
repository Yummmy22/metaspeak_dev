using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Gigadrillgames.AUP.Common
{
    public enum ImageType
    {
        JPG,
        PNG
    }

    public class Utils
    {
        public static void Message(string tag, string message)
        {
            Debug.LogWarning(tag + message);
        }

        //take screen shot then share via intent
        public static IEnumerator TakeScreenshot(string screenShotPath, ImageType imageType)
        {
            yield return new WaitForEndOfFrame();

            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            // Read screen contents into the texture
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            byte[] screenshot;
            if (imageType == ImageType.JPG)
            {
                screenshot = tex.EncodeToJPG();
            }
            else if (imageType == ImageType.PNG)
            {
                screenshot = tex.EncodeToPNG();
            }
            else
            {
                screenshot = tex.EncodeToJPG();
            }

            //saving to phone storage
            File.WriteAllBytes(screenShotPath, screenshot);
        }

        public static IEnumerator TakeScreenshotNoSave(Action<Texture2D> OnComplete)
        {
            yield return new WaitForEndOfFrame();

            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            // Read screen contents into the texture
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);

            tex.Apply();

            if (null != OnComplete)
            {
                OnComplete(tex);
            }
        }

        public static IEnumerator SaveTexureOnDevice(string texturePath, Texture2D texture)
        {
            yield return new WaitForEndOfFrame();

            Color32[] pix = texture.GetPixels32();
            Texture2D destTex = new Texture2D(texture.width, texture.height);
            destTex.SetPixels32(pix);
            destTex.Apply();

            //saving to phone storage
            byte[] existingTexture = destTex.EncodeToPNG();
            File.WriteAllBytes(texturePath, existingTexture);
        }

        public static Texture2D LoadTexture(string imagePath)
        {
            Texture2D tex = new Texture2D(1, 1);

            if (File.Exists(imagePath))
            {
                byte[] bytes = File.ReadAllBytes(imagePath);
                tex.LoadImage(bytes);
            }

            return tex;
        }

        public static IEnumerator LoadTextureFromWeb(string url, Action<Texture2D> OnLoadComplete, Action OnLoadFail)
        {
            // Start a download of the given URL
            UnityWebRequest www = new UnityWebRequest(url);
            DownloadHandlerTexture textDl = new DownloadHandlerTexture();
            www.downloadHandler = textDl;
            // Wait for download to complete
            yield return www.SendWebRequest();
            if (!(www.isNetworkError || www.isHttpError))
            {
                Texture2D t = textDl.texture;
                OnLoadComplete(t);
            }
            else
            {
                OnLoadFail();
            }
        }

        public static T DeepCopy<T>(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;

                return (T) formatter.Deserialize(stream);
            }
        }
        
        public static IEnumerator LoadImage(string url,Action<Texture> LoadTextureHandler, Action LoadTexureFailedHandler)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log($"Utils::LoadImage Error: {www.error}");
                    LoadTexureFailedHandler();
                    www.Dispose();
                }
                else
                {
                    // Get downloaded asset bundle
                    var texture = DownloadHandlerTexture.GetContent(www);
                    LoadTextureHandler(texture);
                    www.Dispose();
                }
            }
        }
        
        public static IEnumerator DownloadFile(string url,string fileName,string extension,Action<string> DownloadCompleteHandler, Action<string> DownloadFailedHandler)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    DownloadFailedHandler(www.error);
                }
                else
                {
                    string savePath = $"{Application.persistentDataPath}/Resources/{fileName}{extension}";        
                    File.WriteAllText(savePath, www.downloadHandler.text);
                    DownloadCompleteHandler($"{fileName}{extension}");
                }
            }
        }

        
        public static IEnumerator LoadAudio(string url, AudioType audioType, Action<AudioClip> LoadAudioClipHandler, Action LoadAudioClipFailedHandler)
        {
            // Start a download of the given URL
            UnityWebRequest www = new UnityWebRequest(url);
            DownloadHandlerAudioClip downloadHandler = new DownloadHandlerAudioClip(url,audioType);
            www.downloadHandler = downloadHandler;
            // Wait for download to complete
            yield return www.SendWebRequest();
            if (!(www.isNetworkError || www.isHttpError))
            {
                AudioClip t = downloadHandler.audioClip;
                LoadAudioClipHandler(t);
                www.Dispose();
            }
            else
            {
                Debug.Log($"Utils::LoadAudio2 Error: {www.error}");
                LoadAudioClipFailedHandler();
                www.Dispose();
            }
        }
        
        
        public static IEnumerator LoadAudio2(string url, AudioType audioType, Action<AudioClip> LoadAudioClipHandler, Action LoadAudioClipFailedHandler)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError )
                {
                    Debug.Log($"Utils::LoadAudio2 Error: {www.error}");
                    LoadAudioClipFailedHandler();
                    www.Dispose();
                }
                else
                {
                   AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                   LoadAudioClipHandler(audioClip);
                   www.Dispose();
                }
            }
        }
        
        public static IEnumerator LoadAudi3(string url, AudioType audioType, Action<AudioClip> LoadAudioClipHandler, Action LoadAudioClipFailedHandler)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError )
                {
                    Debug.Log($"Utils::LoadAudio2 Error: {www.error}");
                    LoadAudioClipFailedHandler();
                    www.Dispose();
                }
                else
                {
                    AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                    LoadAudioClipHandler(audioClip);
                    www.Dispose();
                }
            }
        }
        
        //https://www.youtube.com/watch?v=n_BXpaifIgk
        // https://docs.unity3d.com/ScriptReference/Video.VideoPlayer.html?_ga=2.176851885.90839000.1585401484-1247180066.1584941848
        // https://www.youtube.com/watch?v=SHzzieQvYi0
        //https://forum.unity.com/threads/convert-byte-to-videoclip-and-assign-it-to-a-video-player.522508/

        public static AudioType GetAudioType(String extension)
        {
            switch ( extension)
            {
                case ".mp3":
                    return AudioType.MPEG;
                case ".wav":
                    return AudioType.WAV;
                case ".aiff":
                    return AudioType.AIFF;
                case ".it":
                    return AudioType.IT;
                case ".s3m":
                    return AudioType.S3M;
                case ".xm":
                    return AudioType.XM;
                case ".xma":
                    return AudioType.XMA;
                case ".ogg":
                case ".oga":
                case ".mogg":
                    return AudioType.OGGVORBIS;
                default:
                    return AudioType.UNKNOWN;
            }
        }

        public byte[] GetBytesFromAndroidObject(AndroidJavaObject bytesObj)
        {
            AndroidJavaObject bufferObject = bytesObj.Get<AndroidJavaObject>("Buffer");
            byte[] buffer = AndroidJNIHelper.ConvertFromJNIArray<byte[]>(bufferObject.GetRawObject());
            return buffer;
        }

        // https://stackoverflow.com/questions/16078254/create-audioclip-from-byte
        // https://answers.unity.com/questions/737002/wav-byte-to-audioclip.html
        // https://markheath.net/post/how-to-convert-byte-to-short-or-float
        private float[] ConvertByteToFloat(byte[] array) 
        {
            // usage
            /*
             * AudioClip audioClip = AudioClip.Create("testSound", f.Length, 1, 44100, false, false);
                audioClip.SetData(f, 0);
                AudioSource.PlayClipAtPoint(audioClip, new Vector3(100, 100, 0), 1.0f);
             * 
             */
            float[] floatArr = new float[array.Length / 4];
            for (int i = 0; i < floatArr.Length; i++) 
            {
                if (BitConverter.IsLittleEndian) 
                    Array.Reverse(array, i * 4, 4);
                //floatArr[i] = BitConverter.ToSingle(array, i * 4);
                floatArr[i] = BitConverter.ToSingle(array, i*4) / 0x80000000;
            }
            return floatArr;
        }

        public static long GetTimeSinceEpoch()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        
        public static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
		{

			var samples = new float[clip.samples];

			clip.GetData(samples, 0);

			Int16[] intData = new Int16[samples.Length];
			//converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

			Byte[] bytesData = new Byte[samples.Length * 2];
			//bytesData array is twice the size of
			//dataSource array because a float converted in Int16 is 2 bytes.

			int rescaleFactor = 32767; //to convert float to Int16

			for (int i = 0; i < samples.Length; i++)
			{
				intData[i] = (short) (samples[i] * rescaleFactor);
				Byte[] byteArr = new Byte[2];
				byteArr = BitConverter.GetBytes(intData[i]);
				byteArr.CopyTo(bytesData, i * 2);
			}

			fileStream.Write(bytesData, 0, bytesData.Length);
		}

		public static void WriteHeader(FileStream fileStream, AudioClip clip)
		{

			var hz = clip.frequency;
			var channels = clip.channels;
			var samples = clip.samples;

			fileStream.Seek(0, SeekOrigin.Begin);

			Byte[] riff = Encoding.UTF8.GetBytes("RIFF");
			fileStream.Write(riff, 0, 4);

			Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
			fileStream.Write(chunkSize, 0, 4);

			Byte[] wave = Encoding.UTF8.GetBytes("WAVE");
			fileStream.Write(wave, 0, 4);

			Byte[] fmt = Encoding.UTF8.GetBytes("fmt ");
			fileStream.Write(fmt, 0, 4);

			Byte[] subChunk1 = BitConverter.GetBytes(16);
			fileStream.Write(subChunk1, 0, 4);

			//UInt16 two = 2;
			UInt16 one = 1;

			Byte[] audioFormat = BitConverter.GetBytes(one);
			fileStream.Write(audioFormat, 0, 2);

			Byte[] numChannels = BitConverter.GetBytes(channels);
			fileStream.Write(numChannels, 0, 2);

			Byte[] sampleRate = BitConverter.GetBytes(hz);
			fileStream.Write(sampleRate, 0, 4);

			Byte[]
				byteRate = BitConverter.GetBytes(hz * channels *
				                                 2); // sampleRate * bytesPerSample*number of Channels, here 44100*2*2
			fileStream.Write(byteRate, 0, 4);

			UInt16 blockAlign = (ushort) (channels * 2);
			fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

			UInt16 bps = 16;
			Byte[] bitsPerSample = BitConverter.GetBytes(bps);
			fileStream.Write(bitsPerSample, 0, 2);

			Byte[] datastring = Encoding.UTF8.GetBytes("data");
			fileStream.Write(datastring, 0, 4);

			Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
			fileStream.Write(subChunk2, 0, 4);

			fileStream.Close();
		}
    }
}