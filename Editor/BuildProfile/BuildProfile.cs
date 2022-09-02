#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public class BuildProfile
    {
        private readonly ApplicationProfile _appProfile;
        private readonly string _version;
        private readonly int _buildNumber;

        private readonly ISignProfile _sign;

        public string BuildPath() => _resultPath.BuildPath();
        public string BuildFolderPath() => _resultPath.BuildFolderPath();
        private readonly IResultPathProfile _resultPath;

        public string[] Scenes() => _scenes;
        private readonly string[] _scenes;


        public static BuildProfile WithGooglePlayMarket(ApplicationProfile appProfile, bool buildAppBundle, 
            string version, int buildNumber, string buildFolderPath, IEnumerable<string> scenes, string signJsonFilePath)
        {
            BuildTarget buildTarget = appProfile.BuildTarget();
            ISignProfile sign = AndroidSignProfile.FromJsonFile(buildTarget, signJsonFilePath);
            IResultPathProfile resultPath = new AndroidResultPathProfile(buildTarget, 
                buildAppBundle, buildFolderPath, "GooglePlay", version, buildNumber);
            
            return new BuildProfile(appProfile, version, buildNumber, sign, resultPath, scenes);
        }

        private BuildProfile(ApplicationProfile appProfile, string version, int buildNumber, ISignProfile sign, IResultPathProfile resultPath, IEnumerable<string> scenes)
        {
            _appProfile = appProfile;
            _version = version;
            _buildNumber = buildNumber;
            _sign = sign;
            _resultPath = resultPath;
            _scenes = scenes.ToArray();
        }

        public BuildTarget BuildTarget() => _appProfile.BuildTarget();

        public BuildOptions BuildOptions() => UnityEditor.BuildOptions.None;

        public void Apply()
        {
            PlayerSettings.bundleVersion = _version;
            ApplyBuildNumber();
            _sign.Apply();
            _resultPath.Apply();
        }
        
        private void ApplyBuildNumber()
        {
            BuildTarget target = _appProfile.BuildTarget();
            switch (target)
            {
                case UnityEditor.BuildTarget.Android: 
                    PlayerSettings.Android.bundleVersionCode = _buildNumber; 
                    break;
                
                case UnityEditor.BuildTarget.iOS: 
                    PlayerSettings.iOS.buildNumber = _buildNumber.ToString(); 
                    break;
                
                default:
                    throw new Exception($"{target} not found");
                    
            }
        }
    }
}
