using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveObjectIfBool : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Object;
        [SerializeField] BoolReference _Value;

        private void Update()
        {
            if(_Value.Value)
            {
                _Object.Value.SetActive(true);
            }
            else
            {
                _Object.Value.SetActive(false);
            }
        }
    }
}
