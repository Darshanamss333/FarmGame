using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Wait
    {
        WaitTimer _timer;
        GameObject _object;
        public Wait(float time)
        {
            _object = new GameObject("WaitTimer");
            _timer = _object.AddComponent<WaitTimer>();
            _timer._maxTime = time;
        }

        public GameObject Object
        {
            get
            {
                return _object;
            }
        }

        public UnityAction OnTimeOutAction
        {
            get
            {
                return _timer.OnTimeOutAction;
            }
            set
            {
                _timer.OnTimeOutAction = value;
            }
        }

        public UnityAction OnWaitAction
        {
            get
            {
                return _timer.OnWaitAction;
            }
            set
            {
                _timer.OnWaitAction = value;
            }
        }

        public float CurrentTime
        {
            get
            {
                return _timer._currentTime;
            }
        }

        public float MaxTime
        {
            get
            {
                return _timer._maxTime;
            }
        }

        public float Tang
        {
            get
            {
                return Mathf.InverseLerp(0, _timer._maxTime, _timer._currentTime);
            }
        }
    }

}
