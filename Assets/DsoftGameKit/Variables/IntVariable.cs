using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newInt", menuName = "DsoftGameKit/Int")]
    public class IntVariable : ScriptableObject
    {
        public int Value;

        public void SetValue(int _value)
        {
            Value = _value;
        }

        public void AddValue(int _value)
        {
            Value += _value;
        }
    }
}

