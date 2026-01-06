using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newTransform", menuName = "DsoftGameKit/Transform")]
    public class TransformVariable : ScriptableObject
    {
        public Transform Value;
    }
}
