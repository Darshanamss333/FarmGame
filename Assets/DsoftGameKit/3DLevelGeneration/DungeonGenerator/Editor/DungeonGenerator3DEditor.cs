using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(DungeonGenerator3D))]
    public class DungeonGenerator3DEditor : Editor
    {
        DungeonGenerator3D _target;
        private void Awake()
        {
            _target = (DungeonGenerator3D)target;
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
