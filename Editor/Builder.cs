#nullable enable
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace TeamZero.ApplicationProfile.Building
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

        public void Run(string buildName)
        {
            _profile.Apply();
            TryCreateResultDirectory();
            bool succeeded = UnityProcessBuild(_profile);
            ReportResult(buildName, succeeded);
            TryShowSucceededDialog(buildName);
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
        
        private void TryShowSucceededDialog(string buildName)
        {
            string path = _profile.BuildPath();
            if (Directory.Exists(path) || File.Exists(path))
            {
                if (!EditorUtility.DisplayDialog("Build result", "Build succeeded!", "Close", "Open folder"))
                    EditorUtility.RevealInFinder(path);
            }
            else
                EditorUtility.DisplayDialog($"[{buildName}] Build result", "Build succeeded!", "Close");
        }

        private void ReportResult(string buildName, bool succeeded)
        {
            string message = succeeded ? "Build complete!" : "Build fail!";
            _report.AppendLine($"[{buildName}] {message}");
        }

        private void TryCreateResultDirectory()
        {
            string path = _profile.BuildFolderPath();
            Directory.CreateDirectory(path);
        }
    }
}
