#nullable enable

using System;
using UnityEditor;

namespace TeamZero.ApplicationProfile.Building
{
    public abstract class VersionProfile
    {
        protected readonly Version _version;

        protected VersionProfile(Version version)
        {
            _version = version;
        }

        public virtual void Apply()
        {
            PlayerSettings.bundleVersion = _version.Main();
        }

        public void DrawGUI()
        {
            bool dirty = false;
            string mainVersion = _version.Main();
            string nextMainVersion = EditorGUILayout.TextField("Product Version", mainVersion);
            dirty |= nextMainVersion != mainVersion;

            int buildNumber = _version.BuildNumber();
            int nextBuildNumber = EditorGUILayout.IntField("Build Number", buildNumber);
            nextBuildNumber = Math.Max(0, nextBuildNumber);
            dirty |= nextBuildNumber != buildNumber;

            if(dirty)
                _version.Set(nextMainVersion, nextBuildNumber);
        }
    }
}
