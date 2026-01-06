using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class GameObjectVariableLocal : MonoBehaviour
    {
        [SerializeField] string _Name;
        [SerializeField] GameObjectReference _Object;

        public GameObject Value
        {
            get
            {
                return _Object.Value;
            }
            set
            {
                _Object.Value = value;
            }
        }
    }
}
