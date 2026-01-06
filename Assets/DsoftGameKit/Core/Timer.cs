using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace DsoftGameKit
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] bool _StartOnEnable;
        [SerializeField] FloatReference _Time;
        [SerializeField] bool Repeat;


        float _tang;
        bool _start;
        float _duration;
        public void _StartTimer(float _time)
        {
            _tang = 0;
            _start = true;
            _duration = _time;
        }

        private void Update()
        {
            UpdateTimer();
        }

        public UnityEvent StartCallBack;
        public UnityEvent EndCallBack;
        void UpdateTimer()
        {
            if (_start)
            {
                if (_tang >= _duration)
                {
                    _start = false;
                    EndCallBack.Invoke();
                    if (Repeat) _StartTimer(_duration);
                }
                else
                {
                    if(_tang == 0)
                    {
                        StartCallBack.Invoke();
                    }
                    _tang = Mathf.Clamp(_tang + Time.deltaTime, 0, _duration);
                }
            }
        }

        private void OnEnable()
        {
            if (_StartOnEnable) _StartTimer(_Time.Value);
        }
    }
}

