#nullable enable

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TeamZero.ApplicationProfile.Settings
{
    public class StackTracePlayerSettings : IProfileSettings
    {
        private readonly IDictionary<LogType, StackTraceLogType> _settings;

        public static StackTracePlayerSettings Create(IDictionary<LogType, StackTraceLogType> settings) =>
            new(settings);
        
        private StackTracePlayerSettings(IDictionary<LogType, StackTraceLogType> settings)
        {
            _settings = settings;
        }

        public bool IsSetup()
        {
            foreach (var (logType, stackTraceLogType) in _settings)
            {
                StackTraceLogType currentStackTraceLogType = PlayerSettings.GetStackTraceLogType(logType);
                if (currentStackTraceLogType != stackTraceLogType) return false;
            }
            
            return true;
        }

        public void Setup()
        {
            foreach (var (logType, stackTraceLogType) in _settings)
                PlayerSettings.SetStackTraceLogType(logType, stackTraceLogType);
        }

        private bool _foldoutActive = false;
        public void DrawGUI()
        {
            _foldoutActive = EditorGUILayout.BeginFoldoutHeaderGroup(_foldoutActive, "Stack trace player settings");
            if (_foldoutActive)
            {
                EditorGUI.indentLevel++;
                foreach (var (logType, stackTraceLogType) in _settings)
                    EditorGUILayout.LabelField($"{logType}: {stackTraceLogType}");
                
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
