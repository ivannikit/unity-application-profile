#nullable enable

namespace TeamZero.ApplicationProfile
{
    public readonly struct BuildNameSettings
    {
        public readonly string Prefix;
        public readonly string Postfix;

        public BuildNameSettings(string prefix, string postfix)
        {
            Prefix = prefix;
            Postfix = postfix;
        }
    }
}
