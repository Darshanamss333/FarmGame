using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(CollisionDetect))]
    public class CollisionDetectEditor : Editor
    {
        CollisionDetect _target;
        private void Awake()
        {
            _target = (CollisionDetect)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            switch(_target._CollisionFilter)
            {
                case CollisionDetect.CollisionFilters.ByTag:
                    _target._TagName = EditorGUILayout.TextField("TagName", _target._TagName);
                    break;
            }


            if (GUI.changed)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }
    }
}
