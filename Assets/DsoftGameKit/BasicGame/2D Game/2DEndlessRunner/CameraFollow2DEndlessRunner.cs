using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class CameraFollow2DEndlessRunner : MonoBehaviour
    {
        [SerializeField] GameObject _target;
        private void FixedUpdate()
        {
            float _x = _target.transform.position.x;
            float _y = 0;
            Vector3 _lerpPos = new Vector3(_x, _y, transform.position.z);
            transform.position = _lerpPos;
        }
    }
}
