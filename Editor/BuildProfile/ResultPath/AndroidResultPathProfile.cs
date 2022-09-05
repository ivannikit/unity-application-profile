#nullable enable
using System;
using System.IO;
using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public sealed class AndroidResultPathProfile : IResultPathProfile
    {
        private bool _buildAppBundle;
        private readonly string _buildFolderPath;
        private readonly string _prefix;
        private readonly Version _version;

        public AndroidResultPathProfile(BuildTarget buildTarget, bool buildAppBundle, string buildFolderPath, 
            string prefix, Version version)
        {
            if (buildTarget != BuildTarget.Android)
                throw new Exception($"build target must be {BuildTarget.Android}");

            _buildAppBundle = buildAppBundle;
            _buildFolderPath = buildFolderPath;
            _prefix = prefix;
            _version = version;
        }

        public string BuildFolderPath() => _buildFolderPath;
        public string BuildPath()
        {
            string extension = _buildAppBundle ? "aab" : "apk";
            return Path.Combine(_buildFolderPath, $"{_prefix}-{_version}.{extension}");
        }


        public void Apply()
        {
            EditorUserBuildSettings.buildAppBundle = _buildAppBundle;
        }

        public void DrawGUI()
        {
            _buildAppBundle = EditorGUILayout.Toggle("Build App Bundle", _buildAppBundle);
        }
    }
}
