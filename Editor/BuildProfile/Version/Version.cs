#nullable enable

namespace TeamZero.AppProfileSystem.Editor
{
    public readonly struct Version
    {
        public string Main() => _mainVersion;
        private readonly string _mainVersion;
        
        public int BuildNumber() => _buildNumber;
        private readonly int _buildNumber;

        public Version(string mainVersion, int buildNumber)
        {
            _mainVersion = mainVersion;
            _buildNumber = buildNumber;
        }

        public override string ToString() => $"{_mainVersion}({_buildNumber})";
    }
}
