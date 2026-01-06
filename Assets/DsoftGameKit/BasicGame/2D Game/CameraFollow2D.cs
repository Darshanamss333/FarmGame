using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{

    public class CameraFollow2D : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Target;
        [SerializeField] FloatReference _Smooth;
        private void FixedUpdate()
        {
            Vector3 _lerpPos = Vector3.Lerp(transform.position, new Vector3(_Target.Value.transform.position.x ,_Target.Value.transform.position.y,transform.position.z), Time.deltaTime * _Smooth.Value);
            transform.position = _lerpPos;
        }
    }
}
