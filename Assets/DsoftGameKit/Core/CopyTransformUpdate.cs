using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class CopyTransformUpdate : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Object;
        [SerializeField] GameObjectReference _TargetObject;

        private void Update()
        {
            _SetTransform();
        }

        void _SetTransform()
        {
            if (_Object.Value && _TargetObject.Value)
            {
                _Object.Value.transform.position = _TargetObject.Value.transform.position;
                _Object.Value.transform.rotation = _TargetObject.Value.transform.rotation;
            }
        }
    }
}
