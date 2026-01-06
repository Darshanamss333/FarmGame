using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class LookMovingDirection : MonoBehaviour
    {
        Vector3 _deltaPos;
        private void Update()
        {
            if(_deltaPos != transform.position)
            {
                transform.rotation = Quaternion.LookRotation(transform.position - _deltaPos, Vector3.up);
                _deltaPos = transform.position;
            }
        }

        private void OnEnable()
        {
            _deltaPos = transform.position;
        }
    }
}
