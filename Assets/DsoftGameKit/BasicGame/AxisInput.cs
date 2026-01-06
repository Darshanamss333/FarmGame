using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class AxisInput : MonoBehaviour
    {
        public string InputName;
        public FloatReference Value;
        public FloatReference Sensitive;
        public FloatReference Gravity;

        private void Update()
        {
            float _delta = Input.GetAxis(InputName);
            if (Mathf.Abs(_delta) > 0) Value.Value = Mathf.Lerp(Value.Value, _delta, Time.deltaTime * Sensitive.Value);
            if (_delta == 0) Value.Value = Mathf.Lerp(Value.Value, _delta, Time.deltaTime * Gravity.Value);

            CheckEventSend();
        }

        bool _IsSendEvent;
        public UnityEvent AxisDownEvent;
        public UnityEvent AxisReleaseEvent;
        void CheckEventSend()
        {
            if(!_IsSendEvent && Value.Value > 0.5f)
            {
                AxisDownEvent?.Invoke();
                _IsSendEvent = true;
            }

            if(_IsSendEvent && Value.Value < 0.5f)
            {
                AxisReleaseEvent?.Invoke();
                _IsSendEvent = false;
            }
        }
    }
}
