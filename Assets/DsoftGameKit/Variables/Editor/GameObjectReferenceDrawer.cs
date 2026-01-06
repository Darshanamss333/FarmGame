using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DsoftGameKit;


namespace DsoftGameKit
{
    [CustomPropertyDrawer(typeof(GameObjectReference))]
    public class GameObjectReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            int modeIndex = property.FindPropertyRelative("Mode").intValue;


            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            position.position += Vector2.left * 20;
            var rect = new Rect(position.position, Vector2.one * 20);


            if (EditorGUI.DropdownButton(rect,
                 new GUIContent(EditorGUIUtility.FindTexture("_Popup"))
                , FocusType.Keyboard, new GUIStyle()
                {
                    fixedWidth = 50f,
                    border = new RectOffset(1, 1, 1, 1)
                }))
            {
                GenericMenu menu = new GenericMenu();

                menu.AddItem(new GUIContent("Use Constant"), modeIndex == 0, () => setPropert(property, 0));
                menu.AddItem(new GUIContent("Use Variable"), modeIndex == 1, () => setPropert(property, 1));
                menu.AddItem(new GUIContent("Use LocalVariable"), modeIndex == 2, () => setPropert(property, 2));


                menu.ShowAsContext();
            }

            
            position.position += Vector2.right * 20;

            if (modeIndex == 0)
            {
                EditorGUI.ObjectField(position, property.FindPropertyRelative("Constance"), GUIContent.none);
            }
            else if(modeIndex == 1)
            {
                EditorGUI.ObjectField(position, property.FindPropertyRelative("Variable"), GUIContent.none);
            }
            else
            {
                EditorGUI.ObjectField(position, property.FindPropertyRelative("LocalVariable"), GUIContent.none);
            }

            EditorGUI.EndProperty();
        }

        void setPropert(SerializedProperty property, int value)
        {
            var pro = property.FindPropertyRelative("Mode");
            pro.intValue = value;
            pro.serializedObject.ApplyModifiedProperties();
        }
    }
}
