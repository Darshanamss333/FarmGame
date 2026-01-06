using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class TextShow : MonoBehaviour
    {
        public enum VariableTypeEnum
        {
            _float,
            _Int,
            _string
        }

        public VariableTypeEnum _type;
        public StringReference _string;
        public IntReference _int;
        public FloatReference _float;
        public Text TextUI;
        public void _UpdateText()
        {
            switch (_type)
            {
                case VariableTypeEnum._float:
                    if (TextUI) TextUI.text = _float.Value.ToString();
                    break;
                case VariableTypeEnum._Int:
                    if (TextUI) TextUI.text = _int.Value.ToString();
                    break;
                case VariableTypeEnum._string:
                    if (TextUI) TextUI.text = _string.Value;
                    break;
            }
        }
    }
}