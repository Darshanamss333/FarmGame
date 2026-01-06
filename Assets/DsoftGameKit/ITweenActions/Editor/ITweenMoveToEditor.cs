using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(ITweenMoveTo))]
    public class ITweenMoveToEditor : Editor
    {
        ITweenMoveTo _target;
        private void Awake()
        {
            _target = (ITweenMoveTo)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            _target.Target = (GameObject)EditorGUILayout.ObjectField("Target", _target.Target, typeof(GameObject), true);

            string[] _pathTypesNames = System.Enum.GetNames(typeof(pathTypes));
            _target.PathType = (pathTypes)EditorGUILayout.Popup("PathType", (int)_target.PathType, _pathTypesNames);

            switch (_target.PathType)
            {
                case pathTypes.position:
                    _target.Position = EditorGUILayout.Vector3Field("Position", _target.Position);
                    break;

                case pathTypes.localPath:
                    _target.ClosePath = EditorGUILayout.Toggle("ClosePath", _target.ClosePath);
                    _target.ShowWaypoints = EditorGUILayout.Toggle("ShowWaypoints", _target.ShowWaypoints);
                    _target.LocalPath = PathDraw.DrawInspectorPath(_target.LocalPath, _target.ShowWaypoints , _target.gameObject);
                    break;

                case pathTypes.globlePath:
                    _target.GlobalPath = (Path)EditorGUILayout.ObjectField("Target", _target.GlobalPath, typeof(Path), true);
                    if(_target.GlobalPath) _target.GlobalPath.Points = PathDraw.DrawInspectorPath(_target.GlobalPath.Points, _target.ShowWaypoints, _target.gameObject);
                    break;
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

            if (GUI.changed && !Application.isPlaying)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }


        private void OnSceneGUI()
        {
            switch (_target.PathType)
            {
                case pathTypes.position:
                    if(_target.Target)
                    {
                        Handles.color = Color.white;
                        Handles.DrawWireCube(_target.Target.transform.position, Vector3.one * 3);
                        Handles.Label(_target.Target.transform.position, "Point 0");
                        Handles.DrawWireCube(_target.Position, Vector3.one * 3);
                        Handles.Label(_target.Position, "Point 1");
                        Handles.DrawLine(_target.Target.transform.position, _target.Position);
                        _target.Position = Handles.PositionHandle(_target.Position, Quaternion.identity);
                    }
                    break;

                case pathTypes.localPath:
                    _target.LocalPath = PathDraw.DrawScenePath(_target.LocalPath , _target.ClosePath);
                    break;

                case pathTypes.globlePath:
                    if(_target.GlobalPath) _target.GlobalPath.Points = PathDraw.DrawScenePath(_target.GlobalPath.Points, _target.ClosePath);
                    break;
            }

            if (GUI.changed && !Application.isPlaying)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }
    }

}
