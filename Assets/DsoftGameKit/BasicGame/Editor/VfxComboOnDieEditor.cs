using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(VFXComboOnDie))]
    public class VFXComboOnDieEditor : Editor
    {
        VFXComboOnDie _target;
        private void OnEnable()
        {
            _target = (VFXComboOnDie)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Default"))
            {
                _target._Shake = true;
                _target._CameraShake = true;
                _target._ParticleEffectPrefab = Resources.Load("Particles/DieParticle") as GameObject;
                _target._DisableGameObjectAfterDie = true;
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }
    }

}
