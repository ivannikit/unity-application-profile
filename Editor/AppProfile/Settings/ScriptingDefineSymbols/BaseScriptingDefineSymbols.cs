#nullable enable

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace TeamZero.ApplicationProfile.Settings
{
    internal abstract class BaseScriptingDefineSymbols : IProfileSettings
    {
        private readonly NamedBuildTarget _namedBuildTarget;
        protected readonly HashSet<string> _items;
        
        internal BaseScriptingDefineSymbols(NamedBuildTarget namedBuildTarget, IEnumerable<string> items)
        {
            _namedBuildTarget = namedBuildTarget;
            _items = new HashSet<string>(items);
        }

        protected string[] ActiveDefines()
        {
            PlayerSettings.GetScriptingDefineSymbols(_namedBuildTarget, out string[] defines);
            return defines;
        }

        protected void SetActiveDefines(IEnumerable<string> values)
        {
            string[] defines = values.ToArray();
            PlayerSettings.SetScriptingDefineSymbols(_namedBuildTarget, defines);
        }

        public abstract bool IsSetup();

        public abstract void Setup();

        public abstract void DrawGUI();
        
        public void Add(string value) => _items.Add(value);
        public bool Contains(string value) => _items.Contains(value);
    }
}
