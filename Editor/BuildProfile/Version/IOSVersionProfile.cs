#nullable enable
using UnityEditor;

namespace TeamZero.ApplicationProfile.Building
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
