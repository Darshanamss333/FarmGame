using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class CarCameraFollower : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Target;
        [SerializeField] FloatReference _Distance;
        [SerializeField] FloatReference _Hight;
        [SerializeField] FloatReference _LookYOffset;

        private void LateUpdate()
        {
            if(_Target.Value)
            {
                transform.position = _Target.Value.transform.position + new Vector3(0,_Hight.Value,0) + (_Target.Value.transform.forward * -_Distance.Value);
                transform.rotation = Quaternion.LookRotation((_Target.Value.transform.position + new Vector3(0, _LookYOffset.Value, 0)) - transform.position);
            }
        }

    }
}
