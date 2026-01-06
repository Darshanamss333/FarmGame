using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newVector3", menuName = "DsoftGameKit/Vector3")]
    public class Vector3Variable : ScriptableObject
    {
        public Vector3 Value;

        public void SetValueZero()
        {
            Value = Vector3.zero;
        }
    }
}

