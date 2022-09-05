#nullable enable
using System;
using TeamZero.ApplicationProfile.Building;
using UnityEditor;

namespace TeamZero.ApplicationProfile
{
    public class AppProfile
    {
        public BuildTarget BuildTarget() => _buildTarget;
        private readonly BuildTarget _buildTarget;

        public string[] Scenes() => _scenes;
        private readonly string[] _scenes;

        public static AppProfile Create(BuildTarget buildTarget, string[] scenes) => new(buildTarget, scenes);

        private AppProfile(BuildTarget buildTarget, string[] scenes)
        {
            _buildTarget = buildTarget;
            _scenes = scenes;
        }

        public BuildProfile CreateBuildProfile(Version version, ISignProfile sign)
        {
            switch (_buildTarget)
            {
                case UnityEditor.BuildTarget.Android: 
                    return BuildProfile.ForGooglePlayMarket(this, version, false, sign);
                
                case UnityEditor.BuildTarget.iOS:
                    return BuildProfile.ForIOS(this, version, sign);
                
                default: 
                    throw new NotImplementedException($"BuildTarget {_buildTarget} not found");
            }
        }

        public void DrawGUI()
        {
            EditorGUILayout.LabelField($"BuildTarget: {_buildTarget}");
        }

        public void Apply()
        {
            UnityEngine.Debug.Log("TODO ApplicationProfile Apply");
        }
    }
}
