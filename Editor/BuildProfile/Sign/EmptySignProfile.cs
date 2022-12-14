#nullable enable

namespace TeamZero.ApplicationProfile.Building
{
    public sealed class EmptySignProfile : ISignProfile
    {
        public static EmptySignProfile Create() => new();

        private EmptySignProfile()
        {
        }

        public void Apply()
        {
        }
    }
}
