using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Parallax2D : MonoBehaviour
    {
        Transform _camera;
        float _startPos;
        private void Start()
        {
            _camera = Camera.main.transform;
            _startPos = transform.position.x;
            _parallaxValue = transform.position.z - transform.parent.position.z ;

        }

        float _parallaxValue;
        private void Update()
        {
            float x = (_camera.position.x - _startPos) * _parallaxValue;
            transform.position = new Vector3(_startPos - x, transform.position.y, transform.position.z);
        }
    }

}
