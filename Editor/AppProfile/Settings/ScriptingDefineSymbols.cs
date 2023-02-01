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
        protected readonly IEnumerable<string> _items;
        
        internal BaseScriptingDefineSymbols(NamedBuildTarget namedBuildTarget, IEnumerable<string> items)
        {
            _namedBuildTarget = namedBuildTarget;
            _items = items;
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
    }

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
    
    internal class RemovedScriptingDefineSymbols : BaseScriptingDefineSymbols
    {
        internal RemovedScriptingDefineSymbols(NamedBuildTarget namedBuildTarget, IEnumerable<string> items) 
            : base(namedBuildTarget, items)
        {
        }
        
        public override bool IsSetup()
        {
            HashSet<string> activeDefines = new(ActiveDefines());
            foreach (string item in _items)
            {
                if(activeDefines.Contains(item))
                    return false;
            }

            return true;
        }

        public override void Setup()
        {
            if(IsSetup()) return;
            
            HashSet<string> activeDefines = new(ActiveDefines());
            foreach (string item in _items)
                activeDefines.Remove(item);

            SetActiveDefines(activeDefines);
        }

        public override void DrawGUI()
        {
            foreach (string item in _items)
                EditorGUILayout.LabelField($"[-] {item}");
        }
    }

    public class ScriptingDefineSymbols : IProfileSettings
    {
        private readonly AddingScriptingDefineSymbols _addingSymbols;
        private readonly RemovedScriptingDefineSymbols _removedSymbols;

        public static ScriptingDefineSymbols Create(NamedBuildTarget namedBuildTarget,
            IEnumerable<string>? addingSymbols, IEnumerable<string>? removedSymbols = null)
        {
            addingSymbols ??= new string[0];
            removedSymbols ??= new string[0];
            
            AddingScriptingDefineSymbols addingSymbolsSettings = new(namedBuildTarget, addingSymbols);
            RemovedScriptingDefineSymbols removedSymbolsSettings = new(namedBuildTarget, removedSymbols);

            return new ScriptingDefineSymbols(addingSymbolsSettings, removedSymbolsSettings);
        }
        
        internal ScriptingDefineSymbols(AddingScriptingDefineSymbols addingSymbols, RemovedScriptingDefineSymbols removedSymbols)
        {
            _addingSymbols = addingSymbols;
            _removedSymbols = removedSymbols;
        }

        public bool IsSetup() => _addingSymbols.IsSetup() && _removedSymbols.IsSetup();

        public void Setup()
        {
            _addingSymbols.Setup();
            _removedSymbols.Setup();
        }

        public void DrawGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Scripting Define Symbols:");
            
            EditorGUI.indentLevel++;
            _addingSymbols.DrawGUI();
            _removedSymbols.DrawGUI();
            EditorGUI.indentLevel--;
            
            EditorGUILayout.EndVertical();
        }
    }
}
