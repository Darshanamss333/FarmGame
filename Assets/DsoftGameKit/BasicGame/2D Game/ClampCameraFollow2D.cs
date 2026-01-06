using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{

    public class ClampCameraFollow2D : MonoBehaviour
    {
        public static ClampCameraFollow2D _Instance;

        [SerializeField] GameObject _Target;
        [SerializeField] float _Smooth;
        [SerializeField] GameObject _Room;
        GameObject _deltaRoom;
        private void FixedUpdate()
        {
            float _maxY = _Room.transform.position.y + (_Room.transform.localScale.y * 0.5f);
            float _minY = _Room.transform.position.y - (_Room.transform.localScale.y * 0.5f);
            float _maxX = _Room.transform.position.x + (_Room.transform.localScale.x * 0.5f);
            float _minX = _Room.transform.position.x - (_Room.transform.localScale.x * 0.5f);

            float _camHight = Camera.main.orthographicSize;
            float _camWidth = _camHight * Camera.main.aspect;

            float _MaxPositionX = Mathf.Clamp(_maxX - _camWidth, _Room.transform.position.x, _maxX);
            float _MinPositionX = Mathf.Clamp(_minX + _camWidth, _minX, _Room.transform.position.x);
            float _MaxPositionY = Mathf.Clamp(_maxY - _camHight, _Room.transform.position.y, _maxY);
            float _MinPositionY = Mathf.Clamp(_minY + _camHight, _minY, _Room.transform.position.y);


            float clampX = Mathf.Clamp(_Target.transform.position.x, _MinPositionX, _MaxPositionX);
            float clampY = Mathf.Clamp(_Target.transform.position.y, _MinPositionY, _MaxPositionY);

            Vector3 _clamp = new Vector3(clampX, clampY, transform.position.z);
            Vector3 _lerpPos = Vector3.Lerp(transform.position, _clamp, Time.deltaTime * _Smooth);

            if(_deltaRoom != _Room)
            {
                _lerpPos = _clamp;
                _deltaRoom = _Room;
            }

            transform.position = _lerpPos;
        }

        private void Awake()
        {
            if (_Instance == null) _Instance = this;
        }

        public GameObject Room
        {
            set
            {
                _Room = value;
            }
        }
    }
}
