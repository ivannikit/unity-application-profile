#nullable enable

using System.Collections.Generic;

namespace TeamZero.ApplicationProfile.Settings
{
    public class ProfileSettingsGroup : IProfileSettings
    {
        private readonly IEnumerable<IProfileSettings> _items;

        public ProfileSettingsGroup(IEnumerable<IProfileSettings> items)
        {
            _items = items;
        }

        public bool IsSetup()
        {
            foreach (IProfileSettings item in _items)
            {
                bool isSetup = item.IsSetup();
                if (isSetup == false) return false;
            }

            return true;
        }

        public void Setup()
        {
            foreach (IProfileSettings item in _items)
                item.Setup();
        }

        public void DrawGUI()
        {
            foreach (IProfileSettings item in _items)
                item.DrawGUI();
        }
    }
}
