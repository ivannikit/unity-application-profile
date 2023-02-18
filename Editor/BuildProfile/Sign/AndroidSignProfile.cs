#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace TeamZero.ApplicationProfile.Building
{
    public sealed class AndroidSignProfile : ISignProfile
    {
        private readonly string _keystoreName;
        private readonly string _keystorePass;
        private readonly string _keyaliasName;
        private readonly string _keyaliasPass;

        public static AndroidSignProfile FromJsonFile(BuildTarget buildTarget, string filePath)
        {
            LoadSignFromJsonFile(filePath, out string keystoreName, out string keystorePass, 
                out string keyaliasName, out string keyaliasPass);

            return Create(buildTarget, keystoreName, keystorePass, keyaliasName, keyaliasPass);
        }
        
        private static void LoadSignFromJsonFile(string filePath, out string keystoreName, out string keystorePass, 
            out string keyaliasName, out string keyaliasPass)
        {
            string json = File.ReadAllText(filePath); 
            var data = Core.MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
            if (data == null)
                throw new NullReferenceException(nameof(data));
            
            keystoreName = GetValue(data, "keystoreName");
            keystorePass = GetValue(data, "keystorePass");
            keyaliasName = GetValue(data, "keyaliasName");
            keyaliasPass = GetValue(data, "keyaliasPass");
        }

        private static string GetValue(Dictionary<string, object> data, string key)
        {
            string? value = (string)data[key];
            if(value == null)
                throw new Exception($"key {key} not found");
            return value;
        }
        
        public static AndroidSignProfile Create(BuildTarget buildTarget, string keystoreName, string keystorePass, 
            string keyaliasName, string keyaliasPass)
        {
            if (buildTarget != BuildTarget.Android)
                throw new Exception($"build target must be {BuildTarget.Android}");
            
            return new AndroidSignProfile(keystoreName, keystorePass, keyaliasName, keyaliasPass);
        }
        
        private AndroidSignProfile(string keystoreName, string keystorePass, string keyaliasName, string keyaliasPass)
        {
            _keystoreName = keystoreName;
            _keystorePass = keystorePass;
            _keyaliasName = keyaliasName;
            _keyaliasPass = keyaliasPass;
        }

        public void Apply()
        {
            PlayerSettings.Android.keystoreName = _keystoreName;
            PlayerSettings.Android.keystorePass = _keystorePass;
            PlayerSettings.Android.keyaliasName = _keyaliasName;
            PlayerSettings.Android.keyaliasPass = _keyaliasPass;
        }

        
        [MenuItem("Project/Build Utils/Log android keystore info")]
        private static void LogKeystoreInfo()
        {
            StringBuilder log = new();
            log.AppendLine($"keystoreName: {PlayerSettings.Android.keystoreName}");
            log.AppendLine($"keystorePass: {PlayerSettings.Android.keystorePass}");
            log.AppendLine($"keyaliasName: {PlayerSettings.Android.keyaliasName}");
            log.AppendLine($"keyaliasPass: {PlayerSettings.Android.keyaliasPass}");
            Debug.Log(log.ToString());
        }
    }
}
