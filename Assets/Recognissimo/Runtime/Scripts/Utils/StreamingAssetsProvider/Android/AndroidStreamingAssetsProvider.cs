using System;
using System.Collections;
using System.IO;
using Recognissimo.Utils.StreamingAssetsProvider.Common;
using UnityEngine;
using UnityEngine.Android;

namespace Recognissimo.Utils.StreamingAssetsProvider.Android
{
    /// <summary>
    ///     Helper class for providing StreamingAssets files for Android.
    /// </summary>
    public class AndroidStreamingAssetsProvider : IStreamingAssetsProvider
    {
        private readonly string _containerPath;

        private readonly ObbMounter _obbMounter;

        private readonly IStreamingAssetsProvider _packedStreamingAssetsProvider;

        private ProvisionMode _mode;

        public AndroidStreamingAssetsProvider(string dataPath, string streamingAssetsPath, string persistentDataPath)
        {
            _packedStreamingAssetsProvider = new RemoteStreamingAssetsProvider(streamingAssetsPath, persistentDataPath);

            if (ResolveContainer(dataPath) == Container.Obb)
            {
                _obbMounter = new ObbMounter(dataPath);
                _mode = ProvisionMode.Mount;
            }
            else
            {
                _mode = ProvisionMode.Unpack;
            }
        }

        public IEnumerator Initialize()
        {
            yield return _mode switch
            {
                ProvisionMode.Unpack => PrepareForExtraction(),
                ProvisionMode.Mount => PrepareForMount(),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode))
            };
        }

        public IEnumerator Populate(string streamingAssetsRelativePath, StreamingAssetsProvisionFailedCallback callback)
        {
            switch (_mode)
            {
                case ProvisionMode.Unpack:
                    yield return _packedStreamingAssetsProvider.Populate(streamingAssetsRelativePath, callback);
                    break;
                case ProvisionMode.Mount:
                    yield break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_mode));
            }
        }

        public string Provide(string streamingAssetsRelativePath, StreamingAssetsProvisionFailedCallback callback)
        {
            return _mode switch
            {
                ProvisionMode.Unpack => _packedStreamingAssetsProvider.Provide(streamingAssetsRelativePath, callback),
                ProvisionMode.Mount => Path.Combine(_obbMounter.MountedObbPath(), streamingAssetsRelativePath),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode))
            };
        }

        private IEnumerator PrepareForExtraction()
        {
            yield return _packedStreamingAssetsProvider.Initialize();
        }
    
        private IEnumerator PrepareForMount()
        {
            // if (!AndroidAssetPacks.coreUnityAssetPacksDownloaded && !File.Exists(_containerPath))
            // {
            //     Debug.LogWarning(
            //         "Not all core Unity packages is downloaded, reading language model files may fail");
            //
            //     yield return new WaitUntil(() => File.Exists(_containerPath));
            // }

            if (!AndroidAssetPacks.coreUnityAssetPacksDownloaded)
            {
                Debug.LogWarning("Not all core Unity packages is downloaded, reading language model files may fail");
            }

            yield return new WaitWhile(_obbMounter.IsLoading);

            if (_obbMounter.IsMounted())
            {
                yield break;
            }

            Debug.LogWarning("Cannot mount OBB, entries will be extracted to persistent storage");

            _mode = ProvisionMode.Unpack;
            
            yield return PrepareForExtraction();
        }

        private static Container ResolveContainer(string streamingAssetsPath)
        {
            if (streamingAssetsPath.Contains(".apk"))
            {
                return Container.Apk;
            }

            if (streamingAssetsPath.Contains(".obb"))
            {
                return Container.Obb;
            }

            throw new InvalidOperationException("Cannot detect Android StreamingAssets location.");
        }

        private enum Container
        {
            Apk,
            Obb
        }

        private enum ProvisionMode
        {
            Unpack,
            Mount
        }

        private class ObbMounter
        {
            private const string UnityJavaClassName = "com.unity3d.player.UnityPlayer";

            private const string ThisJavaClassName = "com.bluezzzy.recognissimo.unity.utils.ObbMounter";

            private readonly AndroidJavaObject _obbMounter;

            public ObbMounter(string obbPath)
            {
                using var unityPlayer = new AndroidJavaClass(UnityJavaClassName);
                using var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                _obbMounter = new AndroidJavaObject(ThisJavaClassName, currentActivity, obbPath);
            }

            public bool IsLoading()
            {
                return _obbMounter.Call<bool>("isLoading");
            }

            public bool IsMounted()
            {
                return _obbMounter.Call<bool>("isMounted");
            }

            public string MountedObbPath()
            {
                return _obbMounter.Call<string>("getMountedObbPath");
            }
        }
    }
}