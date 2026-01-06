using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActivePercentage : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Object;
        [SerializeField] FloatReference _PercentageValue;
        private void OnEnable()
        {
            float _value = Random.Range(0, 100);

            if(_value < _PercentageValue.Value)
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
