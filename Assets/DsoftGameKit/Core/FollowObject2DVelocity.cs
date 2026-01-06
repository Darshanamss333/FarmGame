using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class FollowObject2DVelocity : MonoBehaviour
    {
        [SerializeField] Rigidbody2D Rb;
        [SerializeField] GameObjectReference TargetObject;
        [SerializeField] FloatReference Speed;

        private void FixedUpdate()
        {
            Vector3 _dir = (TargetObject.Value.transform.position - Rb.transform.position).normalized;
            Rb.velocity = _dir * Speed.Value;
        }

        private void OnDisable()
        {
            Rb.velocity = Vector2.zero;
        }
    }
}
