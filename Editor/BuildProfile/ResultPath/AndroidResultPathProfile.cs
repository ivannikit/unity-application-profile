#nullable enable
using System;
using System.IO;
using UnityEditor;

namespace TeamZero.ApplicationProfile.Building
{
    public sealed class AndroidResultPathProfile : IResultPathProfile
    {
        private bool _buildAppBundle;
        private readonly string _buildFolderPath;
        private readonly BuildNameSettings _nameSettings;
        private readonly Version _version;

        public AndroidResultPathProfile(BuildTarget buildTarget, bool buildAppBundle, string buildFolderPath, 
            BuildNameSettings nameSettings, Version version)
        {
            if (buildTarget != BuildTarget.Android)
                throw new Exception($"build target must be {BuildTarget.Android}");

            _buildAppBundle = buildAppBundle;
            _buildFolderPath = buildFolderPath;
            _nameSettings = nameSettings;
            _version = version;
        }

        public string BuildFolderPath() => _buildFolderPath;
        public string BuildPath()
        {
            string extension = _buildAppBundle ? "aab" : "apk";
            string fileName = $"{_nameSettings.Prefix}-{_version}_{_nameSettings.Postfix}.{extension}";
            
            return Path.Combine(_buildFolderPath, fileName);
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
