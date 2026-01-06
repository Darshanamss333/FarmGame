using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class LocalPositionAnimation : MonoBehaviour
    {
        [SerializeField] bool _StartOnEnable;
        [SerializeField] bool _IsLoop;
        [SerializeField] bool _StartPosIsZero;

        [SerializeField] Vector3 _Value;
        [SerializeField] float _Time;
        [SerializeField] AnimationCurve _Curve;

        Vector3 _startPos;
        private void Start()
        {
            if (_StartPosIsZero)
            {
                _startPos = Vector3.zero;
            }
            else
            {
                _startPos = transform.localPosition;
            }
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
                _currentTime = Mathf.Clamp(_currentTime + Time.deltaTime, 0, _Time);

                float _tang = Mathf.InverseLerp(0, _Time, _currentTime);
                //transform.localPosition = Vector3.Lerp(_startPos, _startPos + _Value.Value, _Curve.Evaluate(_tang));
                transform.localPosition = _startPos + (_Value * _Curve.Evaluate(_tang));

                if (_currentTime >= _Time)
                {
                    _playing = false;
                    if (_IsLoop) Play();
                }
            }
        }


        private void OnEnable()
        {
            Play();
        }
    }
}