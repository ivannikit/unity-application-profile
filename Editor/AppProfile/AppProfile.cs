#nullable enable

using System;
using System.Linq;
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

        private readonly IProfileSettings _settings;

        public static AppProfile Create(BuildTarget buildTarget, IProfileSettings settings)
        {
            string[] scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
            return new(buildTarget, scenes, settings);
        }

        public static AppProfile Create(BuildTarget buildTarget, string[] scenes, IProfileSettings settings) => 
            new(buildTarget, scenes, settings);

        private AppProfile(BuildTarget buildTarget, string[] scenes, IProfileSettings settings)
        {
            _buildTarget = buildTarget;
            _scenes = scenes;
            _settings = settings;
        }
        
        public BuildProfile CreateBuildProfile(Version version, BuildNameSettings nameSettings)
        {
            ISignProfile sign = EmptySignProfile.Create();
            return CreateBuildProfile(version, sign, nameSettings);
        }

        public BuildProfile CreateBuildProfile(Version version, ISignProfile sign, BuildNameSettings nameSettings)
        {
            switch (_buildTarget)
            {
                case UnityEditor.BuildTarget.Android: 
                    return BuildProfile.ForGooglePlayMarket(this, version, false, sign, nameSettings);
                
                case UnityEditor.BuildTarget.iOS:
                    return BuildProfile.ForIOS(this, version, sign);
                
                default: 
                    throw new NotImplementedException($"{typeof(BuildTarget)} {_buildTarget} not found");
            }
        }

        public void DrawGUI()
        {
            EditorGUILayout.LabelField($"BuildTarget: {_buildTarget}");
            _settings.DrawGUI();
        }

        public void Apply()
        {
            _settings.Setup();
        }

        public bool IsSetup() => _settings.IsSetup();
    }
}
