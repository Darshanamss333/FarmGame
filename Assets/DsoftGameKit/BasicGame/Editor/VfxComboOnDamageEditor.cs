using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(VFXComboOnDamage))]
    public class VFXComboOnDamageEditor : Editor
    {
        VFXComboOnDamage _target;
        private void OnEnable()
        {
            _target = (VFXComboOnDamage)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Default"))
            {
                _target._AudioClip = Resources.Load("Sounds/Damage") as AudioClip;
                _target._Shake = true;
                _target._CameraShake = true;
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }
    }

}
