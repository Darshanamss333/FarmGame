using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class BoolOperator : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += Compare;
        }

        [SerializeField] BoolReference _ValueOne;
        [SerializeField] BoolReference _ValueTwo;

        [SerializeField] UnityEvent OnEqual;
        [SerializeField] UnityEvent OnNotEqual;
        public void Compare()
        {
            if (_ValueOne.Value == _ValueTwo.Value) OnEqual?.Invoke();
            if (_ValueOne.Value != _ValueTwo.Value) OnNotEqual?.Invoke();
        }
    }
}