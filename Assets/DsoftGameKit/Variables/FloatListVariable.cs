using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newListFloat", menuName = "DsoftGameKit/FloatList")]
    public class FloatListVariable : ScriptableObject
    {
        public string Description;
        public List<FloatReference> Value;
    }
}

