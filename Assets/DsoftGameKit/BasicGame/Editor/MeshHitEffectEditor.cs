using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(MeshHitEffect))]
    public class MeshHitEffectEditor : Editor
    {
        MeshHitEffect _target;
        private void OnEnable()
        {
            _target = (MeshHitEffect)target;


        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Default"))
            {
                _target._hitMaterial = Resources.Load("Materials/DamageEffect") as Material;
                _target._Interval.Value = 0.1f;

                _target._Meshes = new List<MeshRenderer>();
                for (int i = 0; i < _target.transform.childCount; i++)
                {
                    if(_target.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
                    {
                        _target._Meshes.Add(_target.transform.GetChild(i).GetComponent<MeshRenderer>());
                    }
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }
    }

}
