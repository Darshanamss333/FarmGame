using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(Path3D))]
    public class Path3DEditor : Editor
    {
        Path3D _target;
        private void Awake()
        {
            _target = (Path3D)target;
        }

        private void OnSceneGUI()
        {
            _target.Points = PathDraw.DrawScenePath(_target.Points, _target.ClosePath);

            if (GUI.changed && !Application.isPlaying)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }
    }

}
