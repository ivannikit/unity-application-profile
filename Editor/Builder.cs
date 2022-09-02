#nullable enable
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using TeamZero.AppProfileSystem.Editor;

namespace TeamZero.AppBuildSystem.Editor
{
    public class Builder
    {
        private readonly BuildProfile _profile;
        private readonly IBuildReport _report;

        public static Builder Create(BuildProfile profile, IBuildReport report) => new(profile, report);
        
        private Builder(BuildProfile profile, IBuildReport report)
        {
            _profile = profile;
            _report = report;
        }

        public void Run()
        {
            _profile.Apply();
            TryCreateResultDirectory();
            bool succeeded = UnityProcessBuild(_profile);
            ReportResult(succeeded);
            TryShowSucceededDialog();
        }

        private static bool UnityProcessBuild(BuildProfile profile)
        {
            BuildPlayerOptions buildOptions;
            try
            {
                buildOptions = new BuildPlayerOptions
                {
                    scenes = profile.Scenes(),
                    locationPathName = profile.BuildPath(),
                    target = profile.BuildTarget(),
                    options = profile.BuildOptions()
                };
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }

            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            bool succeeded = report.summary.result == BuildResult.Succeeded;
            
            return succeeded;
        }
        
        private void TryShowSucceededDialog()
        {
            string path = _profile.BuildPath();
            if (Directory.Exists(path) || File.Exists(path))
            {
                if (!EditorUtility.DisplayDialog("Build result", "Build succeeded!", "Close", "Open folder"))
                    EditorUtility.RevealInFinder(path);
            }
            else
                EditorUtility.DisplayDialog("Build result", "Build succeeded!", "Close");
        }

        private void ReportResult(bool succeeded)
        {
            string message = succeeded ? "Build complete!" : "Build fail!";
            _report.AppendLine(message);
        }

        private void TryCreateResultDirectory()
        {
            string path = _profile.BuildFolderPath();
            Directory.CreateDirectory(path);
        }
    }
}
