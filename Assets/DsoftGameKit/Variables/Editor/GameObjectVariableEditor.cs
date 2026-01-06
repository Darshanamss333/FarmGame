using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DsoftGameKit
{
    [CustomEditor(typeof(GameObjectVariable))]
    public class GameObjectVariableEditor : Editor
    {

        GameObjectVariable _target;

        SerializedProperty _Description;
        SerializedProperty _Type;
        SerializedProperty _Value;
        SerializedProperty _IndexType;
        SerializedProperty _ListIndex;
        SerializedProperty _LocalList;
        SerializedProperty _PriorityList;
        private void OnEnable()
        {
            _target = (GameObjectVariable)target;

            _Description = serializedObject.FindProperty("Description");
            _Type = serializedObject.FindProperty("Type");
            _Value = serializedObject.FindProperty("Value");
            _IndexType = serializedObject.FindProperty("IndexType");
            _ListIndex = serializedObject.FindProperty("ListIndex");
            _LocalList = serializedObject.FindProperty("LocalList");
            _PriorityList = serializedObject.FindProperty("PriorityList");
        }

        public override void OnInspectorGUI()
        {
           //ase.OnInspectorGUI();

            EditorGUILayout.PropertyField(_Description, true);
            EditorGUILayout.PropertyField(_Type, true);

            switch (_target.Type)
            {
                case GameObjectVariable.VariableGetTypeEnum.FromValue:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{Value} Set{Value}");
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.PropertyField(_Value, true);
                    break;

                case GameObjectVariable.VariableGetTypeEnum.FromLocalList:
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.TextField("Get{LocalList} Set{Value]");
                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.PropertyField(_IndexType, true);
                    if (_target.IndexType == GameObjectVariable.IndexTypeEnum.Index)
                    {
                        EditorGUILayout.PropertyField(_ListIndex, true);
                        EditorGUILayout.PropertyField(_LocalList, true);
                    }
                    else if (_target.IndexType == GameObjectVariable.IndexTypeEnum.RandomIndex)
                    {
                        EditorGUILayout.PropertyField(_LocalList, true);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(_PriorityList, true);
                        float _tang = 0;

                        for (int i = 0; i < _target.PriorityList.Count; i++)
                        {
                            _target.PriorityList[i].min = _tang;

                            _tang += _target.PriorityList[i].Priority.Value;
                            _target.PriorityList[i].max = _tang;
                        }
                        _target.maxPriortyValue = _tang;
                    }
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
