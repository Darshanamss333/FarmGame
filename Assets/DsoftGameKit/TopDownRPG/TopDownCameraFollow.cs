using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class TopDownCameraFollow : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Target;
        [SerializeField] FloatReference _OffsetX;
        [SerializeField] FloatReference _OffsetY;
        [SerializeField] FloatReference _OffsetZ;

        private void LateUpdate()
        {
            if (_Target.Value)
            {
                transform.position = new Vector3(_Target.Value.transform.position.x + _OffsetX.Value, _Target.Value.transform.position.y + _OffsetY.Value, _Target.Value.transform.position.z + _OffsetZ.Value);
            }
        }
    }
}
