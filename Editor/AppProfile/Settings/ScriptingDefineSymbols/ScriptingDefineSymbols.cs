#nullable enable

using System;
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
            IEnumerable<string>? addingSymbols = null, IEnumerable<string>? removedSymbols = null)
        {
            addingSymbols ??= Array.Empty<string>();
            removedSymbols ??= Array.Empty<string>();
            
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

        private bool _foldoutActive = true;
        public void DrawGUI()
        {
            _foldoutActive = EditorGUILayout.BeginFoldoutHeaderGroup(_foldoutActive, "Scripting Define Symbols");
            if (_foldoutActive)
            {
                EditorGUI.indentLevel++;
                _addingSymbols.DrawGUI();
                _removedSymbols.DrawGUI();
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        public bool ContainsAddingSymbols(string value) => _addingSymbols.Contains(value);
        public void AppendAddingSymbols(string value) => _addingSymbols.Add(value);
        
        public bool ContainsRemovedSymbols(string value) => _removedSymbols.Contains(value);
        public void AppendRemovedSymbols(string value) => _removedSymbols.Add(value);
        
    }
}
