using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace DsoftGameKit
{
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : PropertyDrawer
    {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            FloatReference.MathTypeEnum typeIndex = (FloatReference.MathTypeEnum)property.FindPropertyRelative("Type").intValue;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            position.height = 20;

            var recttoggle = new Rect(position.position, Vector2.one * 20);
            property.FindPropertyRelative("Advanced").boolValue = EditorGUI.Toggle(recttoggle, property.FindPropertyRelative("Advanced").boolValue);

            
            int modeIndex = property.FindPropertyRelative("Mode").intValue;
            position.position += Vector2.left * 20;
            var rect = new Rect(position.position, Vector2.one * 20);
            if (EditorGUI.DropdownButton(rect,new GUIContent(EditorGUIUtility.FindTexture("_Popup")), FocusType.Keyboard, new GUIStyle()
            {
                fixedWidth = 50f,
                border = new RectOffset(1, 1, 1, 1)
            }))
            {
                GenericMenu menu = new GenericMenu();

                menu.AddItem(new GUIContent("Use Constant"), modeIndex == 0, () => setPropert(property, 0));
                menu.AddItem(new GUIContent("Use Variable"), modeIndex == 1, () => setPropert(property, 1));
                menu.ShowAsContext();
            }

            position.position = position.position + new Vector2(50, 0);
            position.width = position.width - 30;
            if (modeIndex == 0)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("FloatValue"), GUIContent.none);
            }
            else
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("FloatObject"), GUIContent.none);
            }
            
            
            position.position += Vector2.left * 40;
            position.position += Vector2.up * 10;

            if (property.FindPropertyRelative("Advanced").boolValue)
            {
                position.position += Vector2.up * 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("Type"), GUIContent.none);
                switch (typeIndex)
                {

                    case FloatReference.MathTypeEnum.Random:
                        position.position += Vector2.up * 20;
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("Min"), new GUIContent("Min"));

                        position.position += Vector2.up * 20;
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("Max"), new GUIContent("Max"));
                        break;

                    case FloatReference.MathTypeEnum.FromList:
                        position.position += Vector2.up * 20;
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("IndexType"), new GUIContent("IndexType"));


                        FloatReference.IndexTypeEnum _indexType = (FloatReference.IndexTypeEnum)property.FindPropertyRelative("IndexType").intValue;
                        if (_indexType == FloatReference.IndexTypeEnum.FromIndex)
                        {
                            position.position += Vector2.up * 20;
                            EditorGUI.PropertyField(position, property.FindPropertyRelative("Index"), new GUIContent("Index"));
                        }


                        position.position += Vector2.up * 20;
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("FromList"), new GUIContent("FromList"));
                        break;

                    case FloatReference.MathTypeEnum.Add:
                        position.position += Vector2.up * 20;
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("AddList"), new GUIContent("AddList"));
                        break;

                    case FloatReference.MathTypeEnum.Multiply:
                        position.position += Vector2.up * 20;
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("MultiplyList"), new GUIContent("MultiplyList"));
                        break;
                }
            }
            
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float _resualt = 0;
            if (property.FindPropertyRelative("Advanced").boolValue)
            {
                FloatReference.MathTypeEnum typeIndex = (FloatReference.MathTypeEnum)property.FindPropertyRelative("Type").intValue;

                switch (typeIndex)
                {
                    case FloatReference.MathTypeEnum.Random:
                        _resualt = 5 * 22;
                        break;

                    case FloatReference.MathTypeEnum.FromList:

                        FloatReference.IndexTypeEnum _indexType = (FloatReference.IndexTypeEnum)property.FindPropertyRelative("IndexType").intValue;
                        if (_indexType == FloatReference.IndexTypeEnum.FromIndex)
                        {
                            _resualt = property.FindPropertyRelative("FromList").arraySize * 22 + 154;
                        }
                        else
                        {
                            _resualt = property.FindPropertyRelative("FromList").arraySize * 22 + 132;
                        }
                        break;


                    case FloatReference.MathTypeEnum.Add:
                        _resualt = property.FindPropertyRelative("AddList").arraySize * 22 + 110;
                        break;


                    case FloatReference.MathTypeEnum.Multiply:
                        _resualt = property.FindPropertyRelative("MultiplyList").arraySize * 22 + 110;
                        break;
                }
            }
            else
            {
                _resualt = 22;
            }


            return _resualt;
        }



        void setPropert(SerializedProperty property, int value)
        {
            var pro = property.FindPropertyRelative("Mode");
            pro.intValue = value;
            pro.serializedObject.ApplyModifiedProperties();
        }
    }
}


