#nullable enable

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace TeamZero.ApplicationProfile.Settings
{
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
