using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class PlatformerMovement2D : MonoBehaviour
    {
        [SerializeField]
        Rigidbody2D _RB;
        [SerializeField]
        FloatReference _Input;
        [SerializeField]
        FloatReference _Speed;

        private void Update()
        {
            _Input.Value = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            _RB.velocity = new Vector2(_Input.Value * _Speed.Value, _RB.velocity.y);
        }
    }

}
