#nullable enable

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace TeamZero.ApplicationProfile.Settings
{
    internal class AddingScriptingDefineSymbols : BaseScriptingDefineSymbols
    {
        internal AddingScriptingDefineSymbols(NamedBuildTarget namedBuildTarget, IEnumerable<string> items) 
            : base(namedBuildTarget, items)
        {
        }
        
        public override bool IsSetup()
        {
            HashSet<string> activeDefines = new(ActiveDefines());
            foreach (string item in _items)
            {
                if(activeDefines.Contains(item)) continue;
                return false;
            }

            return true;
        }

        public override void Setup()
        {
            if(IsSetup()) return;
            
            HashSet<string> activeDefines = new(ActiveDefines());
            foreach (string item in _items)
                activeDefines.Add(item);

            SetActiveDefines(activeDefines);
        }

        public override void DrawGUI()
        {
            foreach (string item in _items)
                EditorGUILayout.LabelField($"[+] {item}");
        }
    }
}
