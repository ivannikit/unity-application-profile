#nullable enable
using UnityEditor;

namespace TeamZero.AppProfileSystem.Editor
{
    public class ApplicationProfile
    {
        public BuildTarget BuildTarget() => _buildTarget;
        private readonly BuildTarget _buildTarget;

        public static ApplicationProfile Create(BuildTarget buildTarget)
        {
            return new ApplicationProfile(buildTarget);
        }

        private ApplicationProfile(BuildTarget buildTarget)
        {
            _buildTarget = buildTarget;
        }
    }
}
