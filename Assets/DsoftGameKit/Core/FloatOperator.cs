using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class FloatOperator : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += Compare;
        }

        [SerializeField] FloatReference _ValueOne;
        [SerializeField] FloatReference _ValueTwo;

        [SerializeField] UnityEvent OnEqual;
        [SerializeField] UnityEvent OnBiggerThenValueOne;
        [SerializeField] UnityEvent OnSmallThenValueOne;
        public void Compare()
        {
            if (_ValueOne.Value == _ValueTwo.Value) OnEqual?.Invoke();
            if (_ValueOne.Value < _ValueTwo.Value) OnBiggerThenValueOne?.Invoke();
            if (_ValueOne.Value > _ValueTwo.Value) OnSmallThenValueOne?.Invoke();
        }
    }
}