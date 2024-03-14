using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Recognissimo.Editor.BuildUtils
{
    public class WebGL : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder { get; }

        private const string LibraryName = "libRecognissimo.a";

        private const string WasmWorkerFileName = "recognissimo-worker.js";
        
        private const string EmscriptenVersion =
#if UNITY_2022_2_OR_NEWER
            "3.1.8";
#else
            "2.0.19";
#endif

        private static string _currentLibraryPath;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.WebGL)
            {
                return;
            }

            var libraryPathPattern = $"^.+/Recognissimo/.+/WebGL/.+/{LibraryName}$";

            var importers = PluginImporter
                .GetAllImporters()
                .Where(importer => Regex.IsMatch(importer.assetPath, libraryPathPattern));

            var libraryPath = Path.Combine(EmscriptenVersion, LibraryName).Replace("\\", "/");

            foreach (var importer in importers)
            {
                var enable = importer.assetPath.EndsWith(libraryPath);
                importer.SetCompatibleWithPlatform(BuildTarget.WebGL, enable);
                
                if (enable)
                {
                    _currentLibraryPath = Path.GetDirectoryName(importer.assetPath);
                }
            }
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.WebGL)
            {
                return;
            }

            var outputDir = report.summary.outputPath;

            if (!Directory.Exists(outputDir))
            {
                ShowErrorHandlingInstruction("Build output not found.");
            }

            var indexHtmlFiles = Directory.GetFiles(outputDir, "index.html", SearchOption.AllDirectories);

            if (indexHtmlFiles.Length is 0 or > 1)
            {
                ShowErrorHandlingInstruction("Recognissimo cannot find index.html in build directory or there are multiple index.html files.");
            }

            var indexHtmlFilePath = indexHtmlFiles[0];

            var indexHtmlDir = Path.GetDirectoryName(indexHtmlFilePath);

            if (indexHtmlDir == null)
            {
                throw new InvalidOperationException("index.html directory is null.");
            }

            if (_currentLibraryPath == null)
            {
                throw new DirectoryNotFoundException("Directory containing WebGL plugin not found");
            }
            
            var wasmWorkerFilePath = Path.Combine(_currentLibraryPath, WasmWorkerFileName);

            if (!File.Exists(wasmWorkerFilePath))
            {
                throw new FileNotFoundException($"File {wasmWorkerFilePath} not found");
            }
            
            File.Copy(wasmWorkerFilePath, Path.Combine(indexHtmlDir, WasmWorkerFileName), true);
        }

        private static void ShowErrorHandlingInstruction(string errorReason)
        {
            const string instruction =
                "Copy file 'recognissimo-worker.js' to your build directory near index.html";

            EditorUtility.DisplayDialog("Post-build action required", $"{errorReason}\n{instruction}", "Ok");
        }
    }
}