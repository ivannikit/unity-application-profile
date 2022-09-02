#nullable enable

using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public abstract class VersionProfile
    {
        protected readonly Version _version;

        public VersionProfile(Version version)
        {
            _version = version;
        }

        public virtual void Apply()
        {
            PlayerSettings.bundleVersion = _version.Main();
        }
    }
}
