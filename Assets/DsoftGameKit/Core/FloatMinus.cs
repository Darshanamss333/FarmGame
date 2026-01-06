using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class FloatMinus : CommenEvents
    {
        [SerializeField] FloatReference _Float1;
        [SerializeField] FloatReference _Float2;
        [SerializeField] FloatReference _Result;

        bool _Update;

        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += _Minus;
        }

        private void Update()
        {
            if (_Update) _Minus();
        }

        public void _Minus()
        {
            _Result.Value = _Float1.Value - _Float2.Value;
        }
    }
}
