#nullable enable
using System;
using System.IO;
using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public class BuildProfile
    {
        private readonly ApplicationProfile _appProfile;
        private readonly VersionProfile _version;

        private readonly ISignProfile _sign;
        
        public string BuildPath() => _resultPath.BuildPath();
        public string BuildFolderPath() => _resultPath.BuildFolderPath();
        private readonly IResultPathProfile _resultPath;

        public string[] Scenes() => _appProfile.Scenes();

        
        public static BuildProfile ForGooglePlayMarket(ApplicationProfile appProfile, Version version, bool buildAppBundle)
        {
            string projectPath = ProjectPath();
            string buildFolderPath = Path.Combine(projectPath, "Builds");
            string signJsonFilePath = Path.Combine(buildFolderPath, "GooglePlaySign.json");

            BuildTarget buildTarget = appProfile.BuildTarget();
            VersionProfile versionProfile = new AndroidVersionProfile(version);
            ISignProfile sign = AndroidSignProfile.FromJsonFile(buildTarget, signJsonFilePath);
            IResultPathProfile resultPath = new AndroidResultPathProfile(buildTarget, 
                buildAppBundle, buildFolderPath, "GooglePlay", version);

            return new BuildProfile(appProfile, versionProfile, sign, resultPath);
        }
        
        private static string ProjectPath()
        {
            DirectoryInfo? projectDirectory = new DirectoryInfo(UnityEngine.Application.dataPath);
            projectDirectory = projectDirectory.Parent;
            if (projectDirectory == null)
                throw new Exception("Project directory not found");

            return projectDirectory.FullName;
        }

        
        private BuildProfile(ApplicationProfile appProfile, VersionProfile version, ISignProfile sign, IResultPathProfile resultPath)
        {
            _appProfile = appProfile;
            _version = version;
            _sign = sign;
            _resultPath = resultPath;
        }

        public BuildTarget BuildTarget() => _appProfile.BuildTarget();

        public BuildOptions BuildOptions() => UnityEditor.BuildOptions.None;

        public void Apply()
        {
            _version.Apply();
            _sign.Apply();
            _resultPath.Apply();
        }

        public void DrawGUI()
        {
            _version.DrawGUI();
            EditorGUILayout.Space();
            
            _resultPath.DrawGUI();
            EditorGUILayout.Space();
        }
    }
}
