using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class CameraFollow2DPlatformer : MonoBehaviour
    {
        public float _XSmooth = 10;
        public float _YSmooth = 5;
        float _y;
        [SerializeField] GameObject _target;
        private void FixedUpdate()
        {
            float _x = Mathf.Lerp(transform.position.x, _target.transform.position.x, Time.deltaTime * _XSmooth);
            float _y = Mathf.Lerp(transform.position.y, _target.transform.position.y, Time.deltaTime * _YSmooth);
            Vector3 _lerpPos = new Vector3(_x, _y, transform.position.z);
            //if(transform.position.y > _y) _y = _target.transform.position.y;
            transform.position = _lerpPos;
        }

        
    }
}
