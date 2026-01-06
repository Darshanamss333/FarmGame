using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

public class GunHatch : MonoBehaviour 
{
    [SerializeField]
    FloatReference _MoveSpeed;
    [SerializeField]
    Transform _HatchTransform;
    public void Rotate(Vector3 _dir)
    {
        _HatchTransform.rotation = Quaternion.Lerp(_HatchTransform.rotation , Quaternion.LookRotation(_dir,Vector3.up) , Time.deltaTime * _MoveSpeed.Value);
    }
}
