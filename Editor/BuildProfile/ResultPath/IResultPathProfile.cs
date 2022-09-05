#nullable enable

namespace TeamZero.ApplicationProfile.Building
{
    public interface IResultPathProfile
    {
        string BuildFolderPath();
        string BuildPath();

        void Apply();
        void DrawGUI();
    }
}
