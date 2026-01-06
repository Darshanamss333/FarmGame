using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Farmer _Target;
        [SerializeField] Vector3 _Offset;
        private void Start()
        {
            _Offset = new Vector3(6.1f, 14f, -10.8f);
        }
        private void LateUpdate()
        {

            if (_Target == null) _Target = FindObjectOfType<Farmer>();
            if (_Target)
            {
                transform.position = new Vector3(_Target.transform.position.x, _Target.transform.position.y, _Target.transform.position.z) + _Offset;
                transform.rotation = Quaternion.LookRotation(_Target.transform.position - transform.position);
            }
        }
    }
}
