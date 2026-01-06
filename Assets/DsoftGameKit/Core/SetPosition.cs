using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class SetPosition : CommenEvents
    {
        [SerializeField] bool _update;
        [SerializeField] GameObjectReference _Object;
        [SerializeField] GameObjectReference _TargetPos;
        [SerializeField] bool _UpdateUntilPositionSet;
        private void Start()
        {
            _OnAllAction += _SetPosition;
        }

        private void Update()
        {
            if (_update) _SetPosition();

            if(_UpdateUntilPositionSet && _Object.Value)
            {
                _SetPosition();
                _UpdateUntilPositionSet = false;
            }
        }

        public void _SetPosition()
        {
            _Object.Value.transform.position = _TargetPos.Value.transform.position;
        }
    }
}
