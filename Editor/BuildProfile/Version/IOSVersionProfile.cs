#nullable enable

using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public sealed class IOSVersionProfile : VersionProfile
    {
        public IOSVersionProfile(Version version) : base(version)
        {
        }

        public override void Apply()
        {
            base.Apply();
            PlayerSettings.iOS.buildNumber = _version.BuildNumber().ToString(); 
        }
    }
}
