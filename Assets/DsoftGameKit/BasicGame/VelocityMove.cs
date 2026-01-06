using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

[RequireComponent(typeof(Rigidbody))]
public class VelocityMove : MonoBehaviour 
{
    Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _direction)
    {
        //transform.Translate(_direction.x, 0, _direction.z, Space.World);
        _rb.velocity = new Vector3(_direction.x, _rb.velocity.y, _direction.z);
    }
}
