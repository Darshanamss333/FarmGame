using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DsoftGameKit
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : Editor
    {
        FloatVariable _target;

        SerializedProperty _Description;
        SerializedProperty _Type;
        SerializedProperty _Value;
        SerializedProperty _IndexType;
        SerializedProperty _ListIndex;
        SerializedProperty _LocalList;
        SerializedProperty _Min;
        SerializedProperty _Max;
        SerializedProperty _MultiplyList;
        SerializedProperty _AddList;
        private void OnEnable()
        {
            _target = (FloatVariable)target;

            _Description = serializedObject.FindProperty("Description");
            _Type = serializedObject.FindProperty("Type");
            _Value = serializedObject.FindProperty("Value");
            _IndexType = serializedObject.FindProperty("IndexType");
            _ListIndex = serializedObject.FindProperty("ListIndex");
            _LocalList = serializedObject.FindProperty("LocalList");
            _Min = serializedObject.FindProperty("Min");
            _Max = serializedObject.FindProperty("Max");
            _MultiplyList = serializedObject.FindProperty("MultiplyList");
            _AddList = serializedObject.FindProperty("AddList");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUILayout.PropertyField(_Description, true);
            EditorGUILayout.PropertyField(_Type, true);

            switch (_target.Type)
            {
                case FloatVariable.VariableGetTypeEnum.FromValue:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{Value} Set{Value}");
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.PropertyField(_Value, true);
                    break;

                case FloatVariable.VariableGetTypeEnum.FromLocalList:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{LocalList} Set{Value]");
                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.PropertyField(_IndexType, true);
                    if (_target.IndexType == FloatVariable.IndexTypeEnum.Index)
                    {
                        EditorGUILayout.PropertyField(_ListIndex, true);
                        EditorGUILayout.PropertyField(_LocalList, true);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(_LocalList, true);
                    }
                    break;

                case FloatVariable.VariableGetTypeEnum.FromRandomRange:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{RandomRange} Set{Value}");
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.PropertyField(_Min, true);
                    EditorGUILayout.PropertyField(_Max, true);
                    break;

                case FloatVariable.VariableGetTypeEnum.Multiply:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{MultiplyByList} Set{Value}");
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.PropertyField(_Value, true);
                    EditorGUILayout.PropertyField(_MultiplyList, true);
                    break;

                case FloatVariable.VariableGetTypeEnum.Add:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{AddByList} Set{Value}");
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.PropertyField(_Value, true);
                    EditorGUILayout.PropertyField(_AddList, true);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
