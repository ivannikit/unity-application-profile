using TeamZero.AppBuildSystem.Editor;
using UnityEditor;
using UnityEngine;

namespace TeamZero.AppProfileSystem.Editor.GUI
{
    public class AppProfileWindow : EditorWindow
    {
        private ApplicationProfile _appProfile;
        private BuildProfile _buildProfile;
        
        private readonly string[] _toolbarNames = { "Profile", "Builder" };
        private int _toolbarIndex = 0;

        public static void Show(string title, ApplicationProfile appProfile, BuildProfile buildProfile)
        {
            AppProfileWindow window = GetWindow<AppProfileWindow>();
            window.Init(title, appProfile, buildProfile);
        }

        private void Init(string title, ApplicationProfile appProfile, BuildProfile buildProfile)
        {
            titleContent = new GUIContent(title);
            _appProfile = appProfile;
            _buildProfile = buildProfile;
        }

        protected virtual void OnGUI()
        {
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling);
            
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            _toolbarIndex = GUILayout.Toolbar(_toolbarIndex, _toolbarNames);
            GUILayout.EndHorizontal();
            
            if (_toolbarIndex == 0)
                DrawAppProfile();
            else
                DrawBuilder();

            EditorGUI.EndDisabledGroup();
            
            // Repaint after Undo/Redo
            if (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed")
                Repaint();
        }

        private void DrawAppProfile()
        {
            GUILayout.BeginVertical();
            
            EditorGUILayout.Space();
            _appProfile.DrawGUI();
            
            GUILayout.EndVertical();
        }

        private void DrawBuilder()
        {
            GUILayout.BeginVertical();
            
            EditorGUILayout.Space();
            _buildProfile.DrawGUI();
            
            GUILayout.FlexibleSpace();
            if(GUILayout.Button("Build"))
                Build();
            
            GUILayout.EndVertical();
        }

        private void Build()
        {
            IBuildReport report = UnityLogReport.Create();
            Builder builder = Builder.Create(_buildProfile, report);
            builder.Run();
        }
    }
}
