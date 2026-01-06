using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(AssetsSpawner3D))]
    public class AssetsSpawner3DEditor : Editor
    {
        AssetsSpawner3D _target;
        private void Awake()
        {
            _target = (AssetsSpawner3D)target;

            if(!_target.GetComponent<MeshFilter>()) _target.gameObject.AddComponent<MeshFilter>();
            if (!_target.GetComponent<MeshRenderer>()) _target.gameObject.AddComponent<MeshRenderer>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Generate"))
            {
                _target.Generate();
            }

            if (GUI.changed && !Application.isPlaying)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }
    }

}
