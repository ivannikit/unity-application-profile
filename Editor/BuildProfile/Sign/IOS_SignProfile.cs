#nullable enable
using System;
using UnityEditor;

namespace TeamZero.ApplicationProfile.Building
{
    public sealed class IOS_SignProfile : ISignProfile
    {
        private readonly bool _automaticallySign;
        private readonly string _provisionProfileId;
        private readonly ProvisioningProfileType _provisioningProfileType;
        
        public static IOS_SignProfile Create(BuildTarget buildTarget, bool automaticallySign, string provisionProfileId, 
            ProvisioningProfileType provisioningProfileType)
        {
            if (buildTarget != BuildTarget.iOS)
                throw new Exception($"build target must be {BuildTarget.iOS}");
            
            return new IOS_SignProfile(automaticallySign, provisionProfileId, provisioningProfileType);
        }

        private IOS_SignProfile(bool automaticallySign, string provisionProfileId, 
            ProvisioningProfileType provisioningProfileType)
        {
            _automaticallySign = automaticallySign;
            _provisionProfileId = provisionProfileId;
            _provisioningProfileType = provisioningProfileType;
        }

        public void Apply()
        {
            PlayerSettings.iOS.appleEnableAutomaticSigning = _automaticallySign;
            PlayerSettings.iOS.iOSManualProvisioningProfileID = _provisionProfileId;
            PlayerSettings.iOS.iOSManualProvisioningProfileType = _provisioningProfileType;
        }
    }
}
