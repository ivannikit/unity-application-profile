#nullable enable
using System;
using UnityEditor;
using UnityEngine;

namespace TeamZero.AppProfileSystem.Editor
{
    public class ApplicationProfile
    {
        public BuildTarget BuildTarget() => _buildTarget;
        private readonly BuildTarget _buildTarget;

        public string[] Scenes() => _scenes;
        private readonly string[] _scenes;

        public static ApplicationProfile Create(BuildTarget buildTarget, string[] scenes) => new(buildTarget, scenes);

        private ApplicationProfile(BuildTarget buildTarget, string[] scenes)
        {
            _buildTarget = buildTarget;
            _scenes = scenes;
        }

        public BuildProfile CreateBuildProfile(Version version)
        {
            switch (_buildTarget)
            {
                case UnityEditor.BuildTarget.Android: 
                    return BuildProfile.ForGooglePlayMarket(this, version, false);
                
                default: 
                    throw new NotImplementedException($"BuildTarget {_buildTarget} not found");
            }
        }

        public void DrawGUI()
        {
            EditorGUILayout.LabelField($"BuildTarget: {_buildTarget}");
        }
    }
}
