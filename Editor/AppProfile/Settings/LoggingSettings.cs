#if PACKAGE_COM_TEAM_ZERO_UNITY_LOGGING
#nullable enable

using UnityEditor;

namespace TeamZero.ApplicationProfile.Settings
{
    public class LoggingSettings : IProfileSettings
    {
        private const string DISABLE_INFO_LOG = "DISABLE_INFO_LOG";
        private const string DISABLE_WARNING_LOG = "DISABLE_WARNING_LOG";
        private const string DISABLE_ERROR_LOG = "DISABLE_ERROR_LOG";
        
        private readonly ScriptingDefineSymbols _scriptingDefineSymbols;
        private bool _infoEnabled;
        private bool _warningEnabled;
        private bool _errorEnabled;

        public static LoggingSettings Create(ScriptingDefineSymbols scriptingDefineSymbols, 
            bool infoEnabled, bool warningEnabled, bool errorEnabled) =>
                new(scriptingDefineSymbols, infoEnabled, warningEnabled, errorEnabled);

        public LoggingSettings(ScriptingDefineSymbols scriptingDefineSymbols, 
            bool infoEnabled, bool warningEnabled, bool errorEnabled)
        {
            _scriptingDefineSymbols = scriptingDefineSymbols;
            _infoEnabled = infoEnabled;
            _warningEnabled = warningEnabled;
            _errorEnabled = errorEnabled;
        }
        
        public bool IsSetup()
        {
            bool infoEnabledStatus = _scriptingDefineSymbols.ContainsAddingSymbols(DISABLE_INFO_LOG) == false;
            if (_infoEnabled != infoEnabledStatus) return false;
            
            bool warningEnabledStatus = _scriptingDefineSymbols.ContainsAddingSymbols(DISABLE_WARNING_LOG) == false;
            if (_warningEnabled != warningEnabledStatus) return false;
            
            bool errorEnabledStatus = _scriptingDefineSymbols.ContainsAddingSymbols(DISABLE_ERROR_LOG) == false;
            if (_errorEnabled != errorEnabledStatus) return false;

            return true;
        }

        public void Setup()
        {
            if(IsSetup()) return;
            
            if(_infoEnabled)
                _scriptingDefineSymbols.AppendRemovedSymbols(DISABLE_INFO_LOG);
            else
                _scriptingDefineSymbols.AppendAddingSymbols(DISABLE_INFO_LOG);
            
            if(_warningEnabled)
                _scriptingDefineSymbols.AppendRemovedSymbols(DISABLE_WARNING_LOG);
            else
                _scriptingDefineSymbols.AppendAddingSymbols(DISABLE_WARNING_LOG);
            
            if(_errorEnabled)
                _scriptingDefineSymbols.AppendRemovedSymbols(DISABLE_ERROR_LOG);
            else
                _scriptingDefineSymbols.AppendAddingSymbols(DISABLE_ERROR_LOG);
        }

        private bool _foldoutActive = true;
        public void DrawGUI()
        {
            _foldoutActive = EditorGUILayout.BeginFoldoutHeaderGroup(_foldoutActive, "Log settings");
            if (_foldoutActive)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                _infoEnabled = EditorGUILayout.Toggle("info", _infoEnabled);
                _warningEnabled = EditorGUILayout.Toggle("warning", _warningEnabled);
                _errorEnabled = EditorGUILayout.Toggle("error", _errorEnabled);
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
#endif
