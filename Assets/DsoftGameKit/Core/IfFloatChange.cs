using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class IfFloatChange : MonoBehaviour
    {
        [SerializeField] bool DontCallOnEnable;
        [SerializeField] FloatReference _Value;
        [SerializeField] UnityEvent _OnChange;

        float _delta;
        private void Update()
        {
            if(_delta != _Value.Value)
            {
                _OnChange?.Invoke();
                _delta = _Value.Value;
            }
        }

        private void OnEnable()
        {
            if(DontCallOnEnable) _delta = _Value.Value;
        }
    }
}
