#nullable enable

using System;
using System.Reflection;
using UnityEditor;

namespace TeamZero.ApplicationProfile.Settings
{
    public class LunarConsoleSettings : IProfileSettings
    {
        private readonly bool _enabled;

        public LunarConsoleSettings(bool enabled)
        {
            _enabled = enabled;
        }
        
        public bool IsSetup() => _enabled == (bool)EnabledFieldInfo().GetValue(null);

        private FieldInfo? _enabledFieldInfo;
        private FieldInfo EnabledFieldInfo()
        {
            if (_enabledFieldInfo != null) return _enabledFieldInfo;
            
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string assemblyName = assembly.FullName;
                if (assemblyName.StartsWith("UnityEngine")) continue;
                if (assemblyName.StartsWith("UnityEditor")) continue;
                if (assemblyName.StartsWith("System")) continue;
                if (assemblyName.StartsWith("Unity.")) continue;
                if (assemblyName.StartsWith("TeamZero.")) continue;
                
                foreach (TypeInfo typeInfo in assembly.DefinedTypes)
                {
                    if(string.IsNullOrEmpty(typeInfo.FullName)) continue;
                    if (typeInfo.IsClass == false) continue;
                    if (typeInfo.IsPublic == false) continue;
                    
                    if (typeInfo.FullName != null && typeInfo.FullName.Equals(
                            "LunarConsolePluginInternal.LunarConsoleConfig", StringComparison.Ordinal))
                    {
                        FieldInfo? fieldInfo = typeInfo.GetField("consoleEnabled", 
                            BindingFlags.Public | BindingFlags.Static);

                        if (fieldInfo != null)
                        {
                            _enabledFieldInfo = fieldInfo;
                            return _enabledFieldInfo;
                        }
                    }
                }
            }

            throw new NullReferenceException("consoleEnabled field info not found");
        }

        public void Setup()
        {
            if(IsSetup()) return;
            
            object[] parameters = { _enabled };
            SetEnabled_MethodInfo().Invoke(null, parameters);
        }
        
        private MethodInfo? _setEnabled_MethodInfo;
        private MethodInfo SetEnabled_MethodInfo()
        {
            if (_setEnabled_MethodInfo != null) return _setEnabled_MethodInfo;
            
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string assemblyName = assembly.FullName;
                if (assemblyName.StartsWith("UnityEngine")) continue;
                if (assemblyName.StartsWith("UnityEditor")) continue;
                if (assemblyName.StartsWith("System")) continue;
                if (assemblyName.StartsWith("Unity.")) continue;
                if (assemblyName.StartsWith("TeamZero.")) continue;
                
                foreach (TypeInfo typeInfo in assembly.DefinedTypes)
                {
                    if(string.IsNullOrEmpty(typeInfo.FullName)) continue;
                    if (typeInfo.IsClass == false) continue;
                    if (typeInfo.IsPublic == false) continue;
                    
                    if (typeInfo.FullName != null && typeInfo.FullName.Equals(
                            "LunarConsoleEditorInternal.Installer", StringComparison.Ordinal))
                    {
                        MethodInfo? methodInfo = typeInfo.GetMethod("SetLunarConsoleEnabled", 
                            BindingFlags.Public | BindingFlags.Static);

                        if (methodInfo != null)
                        {
                            _setEnabled_MethodInfo = methodInfo;
                            return _setEnabled_MethodInfo;
                        }
                    }
                }
            }

            throw new NullReferenceException("SetLunarConsoleEnabled method info not found");
        }

        public void DrawGUI()
        {
            EditorGUILayout.LabelField($"Lunar Mobile Console enabled: {_enabled}");
        }
    }
}
