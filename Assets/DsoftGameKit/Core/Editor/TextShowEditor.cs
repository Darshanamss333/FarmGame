using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DsoftGameKit
{
    [CustomEditor(typeof(TextShow))]
    public class TextShowEditor : Editor
    {
        TextShow _target;

        SerializedProperty _type;
        SerializedProperty _string;
        SerializedProperty _int;
        SerializedProperty _float;
        SerializedProperty _TextUI;
        private void OnEnable()
        {
            _target = (TextShow)target;

            _type = serializedObject.FindProperty("_type");
            _string = serializedObject.FindProperty("_string");
            _int = serializedObject.FindProperty("_int");
            _float = serializedObject.FindProperty("_float");
            _TextUI = serializedObject.FindProperty("TextUI");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUILayout.PropertyField(_type, true);
            switch (_target._type)
            {
                case TextShow.VariableTypeEnum._string:
                    EditorGUILayout.PropertyField(_string, true);
                    break;

                case TextShow.VariableTypeEnum._Int:
                    EditorGUILayout.PropertyField(_int, true);
                    break;

                case TextShow.VariableTypeEnum._float:
                    EditorGUILayout.PropertyField(_float, true);
                    break;
            }

            EditorGUILayout.PropertyField(_TextUI, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
