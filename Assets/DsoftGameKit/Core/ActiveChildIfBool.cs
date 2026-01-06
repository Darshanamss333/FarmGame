using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveChildIfBool : MonoBehaviour
    {
        [SerializeField] BoolReference _Value;

        GameObject _child;
        private void Update()
        {
            if (!_child) _child = transform.GetChild(0).gameObject;
            if(_Value.Value)
            {
                _child.SetActive(true);
            }
            else
            {
                _child.SetActive(false);
            }
        }
    }
}
