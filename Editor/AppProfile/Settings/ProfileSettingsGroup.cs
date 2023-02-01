#nullable enable

using System.Collections.Generic;
using UnityEngine;

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
            Color defaultColor = UnityEngine.GUI.contentColor;
            foreach (IProfileSettings item in _items)
            {
                Color itemColor = item.IsSetup() ? defaultColor : Color.red;
                UnityEngine.GUI.contentColor = itemColor;
                item.DrawGUI();
            }

            UnityEngine.GUI.contentColor = defaultColor;
        }
    }
}
