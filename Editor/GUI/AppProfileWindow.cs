﻿using UnityEditor;
using UnityEngine;
using TeamZero.ApplicationProfile.Building;

namespace TeamZero.ApplicationProfile.GUI
{
    public sealed class AppProfileWindow : EditorWindow
    {
        private AppProfile _appProfile;
        private BuildProfile _buildProfile;
        
        private readonly string[] _toolbarNames = { "Profile", "Builder" };
        private int _toolbarIndex = 0;

        public static void Show(string title, AppProfile appProfile, BuildProfile buildProfile)
        {
            AppProfileWindow window = GetWindow<AppProfileWindow>();
            window.Init(title, appProfile, buildProfile);
        }

        private void Init(string titleMessage, AppProfile appProfile, BuildProfile buildProfile)
        {
            titleContent = new GUIContent(titleMessage);
            _appProfile = appProfile;
            _buildProfile = buildProfile;
        }

        private void OnGUI()
        {
            if (EditorApplication.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
            {
                Close();
                return;
            }

            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            _toolbarIndex = GUILayout.Toolbar(_toolbarIndex, _toolbarNames);
            GUILayout.EndHorizontal();
            
            if (_toolbarIndex == 0)
                DrawAppProfile();
            else
                DrawBuilder();

            // Repaint after Undo/Redo
            if (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed")
                Repaint();
        }

        private void DrawAppProfile()
        {
            GUILayout.BeginVertical();
            
            EditorGUILayout.Space();
            _appProfile.DrawGUI();
            GUILayout.FlexibleSpace();

            if(GUILayout.Button("Apply")) Apply();
            EditorGUILayout.Space();
            
            GUILayout.EndVertical();
        }

        private void Apply()
        {
            _appProfile.Apply();
        }

        private void DrawBuilder()
        {
            bool disabled = _appProfile.IsSetup() == false;
            EditorGUI.BeginDisabledGroup(disabled);
            EditorGUILayout.BeginVertical();
            
            EditorGUILayout.Space();
            _buildProfile.DrawGUI();
            
            GUILayout.FlexibleSpace();
            if(GUILayout.Button("Build")) Build();
            EditorGUILayout.Space();
            
            EditorGUILayout.EndVertical();
            EditorGUI.EndDisabledGroup();
        }

        private void Build()
        {
            IBuildReport report = UnityLogReport.Create();
            Builder builder = Builder.Create(_buildProfile, report);
            builder.Run();
        }
    }
}
