#nullable enable
using System;
using System.IO;
using UnityEditor;
using TeamZero.ApplicationProfile.Building;

namespace TeamZero.ApplicationProfile
{
    public class BuildProfile
    {
        private readonly AppProfile _appProfile;
        private readonly VersionProfile _version;

        private readonly ISignProfile _sign;
        
        public string BuildPath() => _resultPath.BuildPath();
        public string BuildFolderPath() => _resultPath.BuildFolderPath();
        private readonly IResultPathProfile _resultPath;

        public string[] Scenes() => _appProfile.Scenes();

        
        public static BuildProfile ForGooglePlayMarket(AppProfile appProfile, Version version, 
            bool buildAppBundle, ISignProfile sign, BuildNameSettings nameSettings)
        {
            BuildTarget buildTarget = appProfile.BuildTarget();
            string projectPath = ProjectPath();
            string buildFolderPath = Path.Combine(projectPath, "Builds");
            
            VersionProfile versionProfile = AndroidVersionProfile.Create(version);

            IResultPathProfile resultPath = new AndroidResultPathProfile(buildTarget, 
                buildAppBundle, buildFolderPath, nameSettings, version);

            return new BuildProfile(appProfile, versionProfile, sign, resultPath);
        }
        
        public static BuildProfile ForIOS(AppProfile appProfile, Version version, ISignProfile sign)
        {
            string projectPath = ProjectPath();
            string buildFolderPath = Path.Combine(projectPath, "Builds");
            BuildTarget buildTarget = appProfile.BuildTarget();
            
            VersionProfile versionProfile = new IOSVersionProfile(version);
            IResultPathProfile resultPath = new IOSResultPathProfile(buildTarget, buildFolderPath);
            
            return new BuildProfile(appProfile, versionProfile, sign, resultPath);
        }
        
        public static string ProjectPath()
        {
            DirectoryInfo? projectDirectory = new DirectoryInfo(UnityEngine.Application.dataPath);
            projectDirectory = projectDirectory.Parent;
            if (projectDirectory == null)
                throw new Exception("Project directory not found");

            return projectDirectory.FullName;
        }
        
        private BuildProfile(AppProfile appProfile, VersionProfile version, ISignProfile sign, IResultPathProfile resultPath)
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
