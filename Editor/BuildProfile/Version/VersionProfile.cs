#nullable enable

using UnityEditor;

namespace TeamZero.ApplicationProfile.Building
{
    public abstract class VersionProfile
    {
        protected Version _version;

        public VersionProfile(Version version)
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
            if (nextBuildNumber < 0)
                nextBuildNumber = buildNumber;
            
            dirty |= nextBuildNumber != buildNumber;

            if(dirty)
                _version = new Version(nextMainVersion, nextBuildNumber);
        }
    }
}
