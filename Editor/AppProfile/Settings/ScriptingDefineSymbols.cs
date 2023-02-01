/*
#nullable enable

using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build;

namespace TeamZero.ApplicationProfile.Settings
{
    public class ScriptingDefineSymbols : IProfileSettings
    {
        public bool IsSetup()
        {
            throw new System.NotImplementedException();
        }

        public void Setup()
        {
            throw new System.NotImplementedException();
        }

        public void DrawGUI()
        {
            throw new System.NotImplementedException();
        }
    }
    
    internal abstract class BaseScriptingDefineSymbols : IProfileSettings
    {
        protected readonly IEnumerable<string> _items;

        internal BaseScriptingDefineSymbols(IEnumerable<string> items)
        {
            _items = items;
        }

        protected string[] ActiveDefines() =>
            EditorUserBuildSettings.activeScriptCompilationDefines;

        protected void SetActiveDefines(string[] values)
        {
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Android, values);
        }

        public abstract bool IsSetup();

        public abstract void Setup();

        public abstract void DrawGUI();
    }

    internal class AddingScriptingDefineSymbols : BaseScriptingDefineSymbols
    {
        internal AddingScriptingDefineSymbols(IEnumerable<string> items) : base(items)
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
            HashSet<string> activeDefines = new(ActiveDefines());
            foreach (string item in _items)
                activeDefines.Add(item);
            
            
        }

        public override void DrawGUI()
        {
            throw new System.NotImplementedException();
        }
    }
}
*/
