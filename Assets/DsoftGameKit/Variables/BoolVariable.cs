using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newBool", menuName = "DsoftGameKit/Bool")]
    public class BoolVariable : ScriptableObject
    {

        public bool Value;

        public bool GetSet
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }
    }
}

