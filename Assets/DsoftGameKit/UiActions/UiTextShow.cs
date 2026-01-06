using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class UiTextShow : MonoBehaviour
    {
        public enum VariableTypeEnum
        {
            _float,
            _Int,
            _string,
            _vector3
        }

        public VariableTypeEnum _type;
        public StringReference _string;
        public IntReference _int;
        public FloatReference _float;
        public Vector3Reference _vector3;
        public Text TextUI;
        void _UpdateText()
        {
            switch (_type)
            {
                case VariableTypeEnum._float:
                    if (TextUI) TextUI.text = (_float.Value -  (_float.Value - (int)_float.Value)).ToString();
                    break;
                case VariableTypeEnum._Int:
                    if (TextUI) TextUI.text = _int.Value.ToString();
                    break;
                case VariableTypeEnum._string:
                    if (TextUI) TextUI.text = _string.Value;
                    break;
                case VariableTypeEnum._vector3:
                    if (TextUI) TextUI.text = _vector3.Value.ToString();
                    break;
            }
        }

        private void Update()
        {
            _UpdateText();
        }

        public void ChangeString(string _value)
        {
            _string.Value = _value;
        }
    }
}