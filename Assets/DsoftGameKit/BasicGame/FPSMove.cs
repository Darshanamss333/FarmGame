using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class FPSMove : MonoBehaviour
    {
        //Move-------------------------------------
        [SerializeField]
        FloatReference _speed;
        [SerializeField]
        Vector3Reference _input;
        [SerializeField]
        Rigidbody MoveRB;
        void Move()
        {
            Vector3 _normalInput = _input.Value.normalized * _speed.Value;
            MoveRB.velocity = transform.TransformDirection(_normalInput.x, MoveRB.velocity.y, _normalInput.y);
        }

        private void FixedUpdate()
        {
            Move();
        }
    }
}
