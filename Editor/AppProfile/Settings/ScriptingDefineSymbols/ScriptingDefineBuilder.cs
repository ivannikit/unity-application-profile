#nullable enable

using System.Collections.Generic;
using UnityEditor.Build;

namespace TeamZero.ApplicationProfile.Settings
{
    public class ScriptingDefineBuilder
    {
        private readonly NamedBuildTarget _buildTarget;
        private readonly HashSet<string> _addingSymbols = new();
        private readonly HashSet<string> _removedSymbols = new();

        public static ScriptingDefineBuilder Create(NamedBuildTarget buildTarget) => new (buildTarget);
        
        private ScriptingDefineBuilder(NamedBuildTarget buildTarget)
        {
            _buildTarget = buildTarget;
        }
        
        public ScriptingDefineSymbols ToSettings() => 
            ScriptingDefineSymbols.Create(_buildTarget, _addingSymbols, _removedSymbols);

        public ScriptingDefineBuilder AppendAddingSymbols(string value)
        {
            _addingSymbols.Add(value);
            return this;
        }
        
        public ScriptingDefineBuilder AppendRemovedSymbols(string value)
        {
            _removedSymbols.Add(value);
            return this;
        }
    }
}
