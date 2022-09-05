#nullable enable
using System;
using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public sealed class IOS_SignProfile : ISignProfile
    {
        public static IOS_SignProfile Create(BuildTarget buildTarget)
        {
            if (buildTarget != BuildTarget.iOS)
                throw new Exception($"build target must be {BuildTarget.iOS}");
            
            return new IOS_SignProfile();
        }
        
        private IOS_SignProfile()
        {
        }

        public void Apply()
        {
        }
    }
}
