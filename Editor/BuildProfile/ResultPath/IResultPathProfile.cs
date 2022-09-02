#nullable enable

namespace TeamZero.AppProfileSystem.Editor
{
    public interface IResultPathProfile
    {
        string BuildFolderPath();
        string BuildPath();

        void Apply();
    }
}
