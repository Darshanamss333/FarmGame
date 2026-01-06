using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class SetPositionVector3 : CommenEvents
    {
        [SerializeField] bool _update;
        [SerializeField] GameObjectReference _Object;
        [SerializeField] Vector3Reference _TargetPos;
        private void Start()
        {
            _OnAllAction += _SetPosition;
        }

        private void Update()
        {
            if (_update) _SetPosition();
        }

        public void _SetPosition()
        {
            _Object.Value.transform.position = _TargetPos.Value;
        }
    }
}
