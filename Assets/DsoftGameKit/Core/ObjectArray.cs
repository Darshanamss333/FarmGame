using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ObjectArray : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();

            _OnAllAction += SpawnArrayObjects;
        }

        [SerializeField] GameObjectReference _Object;
        [SerializeField] FloatReference _Count;
        [SerializeField] Vector3Reference _Offset;
        public void SpawnArrayObjects()
        {
            for (int i = 0; i < _Count.Value; i++)
            {
                GameObject _new = Instantiate(_Object.Value);
                _new.transform.position = transform.position + (_Offset.Value * i);
                _new.transform.parent = transform;
            }
        }
    }
}
