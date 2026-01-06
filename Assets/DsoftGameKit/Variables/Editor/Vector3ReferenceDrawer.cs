using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DsoftGameKit;


namespace DsoftGameKit
{
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    public class Vector3ReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            int usetype = property.FindPropertyRelative("Type").intValue;


            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            position.position += Vector2.left * 20;
            var rect = new Rect(position.position, Vector2.one * 20);


            if (EditorGUI.DropdownButton(rect,
                 new GUIContent(EditorGUIUtility.FindTexture("_Popup"))
                ,FocusType.Keyboard,new GUIStyle()
            {
                fixedWidth = 50f ,border = new RectOffset(1,1,1,1)
            }))
            {
                GenericMenu menu = new GenericMenu();

                menu.AddItem(new GUIContent("Use Constant"), usetype == 0, () => setPropert(property, 0));
                menu.AddItem(new GUIContent("Use Variable"), usetype == 1, () => setPropert(property, 1));
                menu.AddItem(new GUIContent("Use Float"), usetype == 2, () => setPropert(property, 2));
                menu.ShowAsContext();
            }

            position.position += Vector2.right * 20;
            Vector3 value = property.FindPropertyRelative("Constance").vector3Value;
               
            if(usetype == 0)
            {
                property.FindPropertyRelative("Constance").vector3Value = EditorGUI.Vector3Field(position, "", value);
            }
            else if(usetype == 1)
            {
                EditorGUI.ObjectField(position, property.FindPropertyRelative("Variable"), GUIContent.none);
            }
            else
            {
                position.height = 20;

                position.position += Vector2.up * 22;
                EditorGUI.LabelField(position, "X");
                EditorGUI.PropertyField(position, property.FindPropertyRelative("X"), GUIContent.none);

                position.position += Vector2.up * 22;
                EditorGUI.LabelField(position, "Y");
                EditorGUI.PropertyField(position, property.FindPropertyRelative("Y"), GUIContent.none);

                position.position += Vector2.up * 22;
                EditorGUI.LabelField(position, "Z");
                EditorGUI.PropertyField(position, property.FindPropertyRelative("Z"), GUIContent.none);
            }

            EditorGUI.EndProperty();
        }

        void setPropert(SerializedProperty property, int value)
        {
            var pro = property.FindPropertyRelative("Type");
            pro.intValue = value;
            pro.serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int usetype = property.FindPropertyRelative("Type").intValue;

            if(usetype == 0)
            {
                return 22;
            }
            else if(usetype == 1)
            {
                return 22;
            }
            else
            {
                return 88;
            }
        }
    }
}


