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
        private readonly VersionProfile _version;

        private readonly ISignProfile _sign;

        public string BuildPath() => _resultPath.BuildPath();
        public string BuildFolderPath() => _resultPath.BuildFolderPath();
        private readonly IResultPathProfile _resultPath;

        public string[] Scenes() => _scenes;
        private readonly string[] _scenes;


        public static BuildProfile WithGooglePlayMarket(ApplicationProfile appProfile, bool buildAppBundle, 
            Version version, string buildFolderPath, IEnumerable<string> scenes, string signJsonFilePath)
        {
            BuildTarget buildTarget = appProfile.BuildTarget();
            VersionProfile versionProfile = new AndroidVersionProfile(version);
            ISignProfile sign = AndroidSignProfile.FromJsonFile(buildTarget, signJsonFilePath);
            IResultPathProfile resultPath = new AndroidResultPathProfile(buildTarget, 
                buildAppBundle, buildFolderPath, "GooglePlay", version);

            return new BuildProfile(appProfile, versionProfile, sign, resultPath, scenes);
        }

        private BuildProfile(ApplicationProfile appProfile, VersionProfile version, ISignProfile sign, IResultPathProfile resultPath, IEnumerable<string> scenes)
        {
            _appProfile = appProfile;
            _version = version;
            _sign = sign;
            _resultPath = resultPath;
            _scenes = scenes.ToArray();
        }

        public BuildTarget BuildTarget() => _appProfile.BuildTarget();

        public BuildOptions BuildOptions() => UnityEditor.BuildOptions.None;

        public void Apply()
        {
            _version.Apply();
            _sign.Apply();
            _resultPath.Apply();
        }
    }
}
