using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace DsoftGameKit
{
    [CustomPropertyDrawer(typeof(SubFloatReference))]
    public class SubFloatReferenceDrawer : PropertyDrawer
    {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {            
            EditorGUI.BeginProperty(position, label, property);

            bool useType = property.FindPropertyRelative("Type").boolValue;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            position.position += Vector2.left * 20;
            var GearRect = new Rect(position.position, Vector2.one * 20);


            if (EditorGUI.DropdownButton(GearRect, new GUIContent(EditorGUIUtility.FindTexture("_Popup")), FocusType.Keyboard, new GUIStyle()
            {
                fixedWidth = 50f,
                border = new RectOffset(1, 1, 1, 1)
            }))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Use Constant"), useType, () => setPropert(property, true));
                menu.AddItem(new GUIContent("Use Variable"), !useType, () => setPropert(property, false));

                menu.ShowAsContext();
            }


            position.position += Vector2.right * 20;

            if (useType)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("Constance"), GUIContent.none);
            }
            else 
            {
                EditorGUI.ObjectField(position, property.FindPropertyRelative("Variable"), GUIContent.none);
            }
            

            EditorGUI.EndProperty();
        }
        

        void setPropert(SerializedProperty property, bool value)
        {
            var pro = property.FindPropertyRelative("Type");
            pro.boolValue = value;
            pro.serializedObject.ApplyModifiedProperties();
        }
    }
}


