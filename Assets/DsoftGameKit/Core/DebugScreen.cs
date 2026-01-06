using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Text))]
    public class DebugScreen : MonoBehaviour
    {
        public static DebugScreen _instance;
        private void Awake()
        {
            if(!_instance)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _text = GetComponent<Text>();
        }

        Text _text;
        public void Debug(string _value)
        {
            _text.text = _text.text + "\n" + _value; 
        }
    }
}
