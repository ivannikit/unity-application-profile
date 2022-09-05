#nullable enable
using System;
using System.IO;
using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public sealed class IOSResultPathProfile : IResultPathProfile
    {
        private readonly string _buildPath;

        public IOSResultPathProfile(BuildTarget buildTarget, string buildFolderPath)
        {
            if (buildTarget != BuildTarget.iOS)
                throw new Exception($"build target must be {BuildTarget.iOS}");

            _buildPath = Path.Combine(buildFolderPath, "iOS");
        }

        public string BuildFolderPath() => _buildPath;
        public string BuildPath() => _buildPath;


        public void Apply()
        {
        }

        public void DrawGUI()
        {
        }
    }
}
