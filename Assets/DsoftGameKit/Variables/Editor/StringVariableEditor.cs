using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DsoftGameKit
{
    [CustomEditor(typeof(StringVariable))]
    public class StringVariableEditor : Editor
    {
        StringVariable _target;

        SerializedProperty _Description;
        SerializedProperty _Type;
        SerializedProperty _Value;
        SerializedProperty _IndexType;
        SerializedProperty _ListIndex;
        SerializedProperty _LocalList;
        private void OnEnable()
        {
            _target = (StringVariable)target;

            _Description = serializedObject.FindProperty("Description");
            _Type = serializedObject.FindProperty("Type");
            _Value = serializedObject.FindProperty("Value");
            _IndexType = serializedObject.FindProperty("IndexType");
            _ListIndex = serializedObject.FindProperty("ListIndex");
            _LocalList = serializedObject.FindProperty("LocalList");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUILayout.PropertyField(_Description, true);
            EditorGUILayout.PropertyField(_Type, true);

            switch (_target.Type)
            {
                case StringVariable.VariableGetTypeEnum.FromValue:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{Value} Set{Value}");
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.PropertyField(_Value, true);
                    break;

                case StringVariable.VariableGetTypeEnum.FromLocalList:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{LocalList} Set{Value]");
                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.PropertyField(_IndexType, true);
                    if (_target.IndexType == StringVariable.IndexTypeEnum.Index)
                    {
                        EditorGUILayout.PropertyField(_ListIndex, true);
                        EditorGUILayout.PropertyField(_LocalList, true);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(_LocalList, true);
                    }
                    break;

                case StringVariable.VariableGetTypeEnum.FromRandomRange:
                    GUI.backgroundColor = Color.red;
                    EditorGUILayout.TextField("Get{Null} Set{Value}");
                    GUI.backgroundColor = Color.white;
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
