#nullable enable
using UnityEditor;

namespace TeamZero.ApplicationProfile.Building
{
    public sealed class AndroidVersionProfile : VersionProfile
    {
        public static AndroidVersionProfile Create(Version version) => new(version);

        private AndroidVersionProfile(Version version) : base(version)
        {
        }

        
        public override void Apply()
        {
            base.Apply();
            PlayerSettings.Android.bundleVersionCode = _version.BuildNumber(); 
        }
    }
}
