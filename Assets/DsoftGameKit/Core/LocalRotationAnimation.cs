using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class LocalRotationAnimation : MonoBehaviour
    {
        [SerializeField] bool _StartOnEnable;
        [SerializeField] bool _Loop;
        [SerializeField] Vector3Reference _Value;
        [SerializeField] FloatReference _Time;
        [SerializeField] AnimationCurve _Curve;

        Quaternion _startRot;
        private void Start()
        {
            _startRot = transform.localRotation;
        }

        bool _playing;
        public void Play()
        {
            if(!_playing)
            {
                _currentTime = 0;
                _playing = true;
            }
        }

        float _currentTime;
        private void Update()
        {
            if(_playing)
            {
                _currentTime = Mathf.Clamp(_currentTime + Time.deltaTime, 0, _Time.Value);

                float _tang = Mathf.InverseLerp(0, _Time.Value, _currentTime);
                //transform.localRotation = Quaternion.Lerp(_startRot, _startRot * Quaternion.Euler(_Value.Value), _Curve.Evaluate(_tang));
                transform.localRotation = _startRot *  Quaternion.Euler(_Value.Value * _Curve.Evaluate(_tang));

                if (_currentTime >= _Time.Value)
                {
                    if(_Loop)
                    {
                        _playing = false;
                        Play();
                    }
                    else
                    {
                        _playing = false;
                    }
                }
            }
        }

        private void OnEnable()
        {
            if (_StartOnEnable) Play();
        }
    }
}