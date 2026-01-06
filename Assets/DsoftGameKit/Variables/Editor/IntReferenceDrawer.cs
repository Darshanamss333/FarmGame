using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DsoftGameKit;

namespace DsoftGameKit
{
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            bool useConstance = property.FindPropertyRelative("IsConstant").boolValue;

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
                menu.AddItem(new GUIContent("Use Constant"),useConstance,() => setPropert(property, true));
                menu.AddItem(new GUIContent("Use Variable"), !useConstance, () => setPropert(property, false));

                menu.ShowAsContext();
            }

            position.position += Vector2.right * 20;
            int value = property.FindPropertyRelative("Constance").intValue;
               

            if(useConstance)
            {
                string newValue = EditorGUI.TextField(position, value.ToString());
                int.TryParse(newValue, out value);
                property.FindPropertyRelative("Constance").intValue = value;
            }
            else
            {
                EditorGUI.ObjectField(position, property.FindPropertyRelative("Variable"), GUIContent.none);
            }

            EditorGUI.EndProperty();
        }
        
        void setPropert(SerializedProperty property , bool value)
        {
            var pro = property.FindPropertyRelative("IsConstant");
            pro.boolValue = value;
            pro.serializedObject.ApplyModifiedProperties();
        }

    }
}


