#nullable enable

namespace TeamZero.ApplicationProfile
{
    public interface IProfileSettings
    {
        bool IsSetup();
        void Setup();
        void DrawGUI();
    }
}
