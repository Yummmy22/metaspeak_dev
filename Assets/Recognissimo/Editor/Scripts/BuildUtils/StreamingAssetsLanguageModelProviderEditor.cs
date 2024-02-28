using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Recognissimo.Utils;
using Recognissimo.Utils.Network;
using Recognissimo.Utils.StreamingAssetsProvider.Common;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Recognissimo.Editor.BuildUtils
{
    public class StreamingAssetsLanguageModelProvider : IPreprocessBuildWithReport,
        IPostprocessBuildWithReport
    {
        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform is BuildTarget.Android or BuildTarget.WebGL)
            {
                RemoveStreamingAssetsManifest();
            }
        }

        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform is BuildTarget.Android or BuildTarget.WebGL)
            {
                GenerateStreamingAssetsManifest();
            }
        }

        private static void GenerateStreamingAssetsManifest()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                return;
            }

            var manifest = StreamingAssetsManifestGenerator.Generate();
            var manifestSavePath = Path.Combine(Application.streamingAssetsPath,
                RemoteStreamingAssetsProvider.StreamingAssetsManifestName);
            File.WriteAllText(manifestSavePath, Json.Serialize(manifest));
        }

        private static void RemoveStreamingAssetsManifest()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                return;
            }

            var manifestSavePath = Path.Combine(Application.streamingAssetsPath,
                RemoteStreamingAssetsProvider.StreamingAssetsManifestName);
            File.Delete(manifestSavePath);
            var manifestMetaPath = manifestSavePath + ".meta";
            File.Delete(manifestMetaPath);
        }
    }

    internal static class StreamingAssetsManifestGenerator
    {
        private static bool IsFileValuable(string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            return Path.GetExtension(fileName) != ".meta" && !fileName.StartsWith('.');
        }

        private static string GenerateFileVersion(string path)
        {
            var lastWriteTime = File.GetLastWriteTime(path);

            return lastWriteTime.ToFileTimeUtc() != 0
                ? lastWriteTime.ToString("O")
                : new Guid().ToString();
        }

        private static IEnumerable<RemoteFile> Enumerate()
        {
            var root = $"{Application.streamingAssetsPath}/";

            if (!Directory.Exists(root))
            {
                return new List<RemoteFile>();
            }

            return Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
                .Where(IsFileValuable)
                .Select(path => new RemoteFile
                {
                    url = path.Replace("\\", "/").Replace(root, ""),
                    version = GenerateFileVersion(path)
                });
        }

        public static RemoteFilesManifest Generate()
        {
            return new RemoteFilesManifest
            {
                content = Enumerate().ToList()
            };
        }
    }
}