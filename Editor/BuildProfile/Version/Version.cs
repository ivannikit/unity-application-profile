using System;
using UnityEditor;

#nullable enable

namespace TeamZero.ApplicationProfile
{
    public sealed class Version
    {
        public string Main() => _mainVersion;
        private string _mainVersion;
        
        public int BuildNumber() => _buildNumber;
        private int _buildNumber;


        public static Version FromAndroidPlayerSettings()
        {
            string mainVersion = PlayerSettings.bundleVersion;
            int buildNumber = PlayerSettings.Android.bundleVersionCode;

            return new Version(mainVersion, buildNumber);
        }
        
        public static Version FromIOSPlayerSettings()
        {
            string mainVersion = PlayerSettings.bundleVersion;
            string buildNumberString = PlayerSettings.iOS.buildNumber;
            int buildNumber = Int32.Parse(buildNumberString);

            return new Version(mainVersion, buildNumber);
        }

        public static Version Create(string mainVersion, int buildNumber) => new(mainVersion, buildNumber);
        
        private Version(string mainVersion, int buildNumber)
        {
            _mainVersion = mainVersion;
            _buildNumber = buildNumber;
        }

        public override string ToString() => $"{_mainVersion}({_buildNumber})";

        public void Set(string mainVersion, int buildNumber)
        {
            _mainVersion = mainVersion;
            _buildNumber = buildNumber;
        }
    }
}
