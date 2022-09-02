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

        public static Builder Create(BuildProfile profile, IBuildReport report) => 
            new(profile, report);
        
        private Builder(BuildProfile profile, IBuildReport report)
        {
            _profile = profile;
            _report = report;
        }

        public void Run()
        {
            _profile.Apply();
            TryCreateResultDirectory();
            
            BuildPlayerOptions buildOptions;
            try
            {
                buildOptions = new BuildPlayerOptions
                {
                    scenes = _profile.Scenes(),
                    locationPathName = _profile.ResultPath(),
                    target = _profile.BuildTarget(),
                    options = _profile.BuildOptions()
                };
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return;
            }

            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            bool succeeded = report.summary.result == BuildResult.Succeeded;
            ReportResult(succeeded);
        }

        private void ReportResult(bool succeeded)
        {
            string message = succeeded ? "Build complete!" : "Build fail!";
            _report.AppendLine(message);
        }

        private void TryCreateResultDirectory()
        {
            string path = _profile.ResultPath();
            string? directory = Path.GetDirectoryName(path);
            if(directory == null)
                throw new NullReferenceException();
            
            Directory.CreateDirectory(directory);
        }
    }
}
