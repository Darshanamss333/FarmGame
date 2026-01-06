using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Blink : MonoBehaviour
    {
        [SerializeField] FloatReference _BlinkInterval;
        [SerializeField] GameObjectReference _Object;

        float _tang;
        private void Update()
        {
            if (_tang < _BlinkInterval.Value)
            {
                _tang += Time.deltaTime;
            }
            else
            {
                _tang = 0;
                _Object.Value.SetActive(!_Object.Value.active);
            }
        }
    }
}
