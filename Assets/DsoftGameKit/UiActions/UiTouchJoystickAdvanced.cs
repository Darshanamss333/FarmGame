using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class UiTouchJoystickAdvanced : UiTouchBehaviourEvents
    {
        [SerializeField] int _InputIndex;
        [SerializeField] GameObject _JoyStickUiOuter;
        [SerializeField] GameObject _JoyStickUiInner;
        private void OnEnable()
        {
            EasyInputManager._Instance._Data._Joysticks[_InputIndex] = Vector3.zero; 
        }

        private void OnDisable()
        {
            EasyInputManager._Instance._Data._Joysticks[_InputIndex] = Vector3.zero;
        }

        protected override void OnTouchDown(InputClass _data)
        {
            base.OnTouchDown(_data);
            EasyInputManager._Instance._Data._Joysticks[_InputIndex] = Vector3.zero;
            _JoyStickUiOuter.transform.position = _data._CurrentPos;
            _JoyStickUiInner.transform.position = _data._CurrentPos;
        }

        protected override void OnTouchHold(InputClass _data)
        {
            base.OnTouchHold(_data);
            Vector3 _dir = (_data._CurrentPos - _data._StartPos).normalized;
            float _value = 30f / 600f * Screen.height;
            _JoyStickUiInner.transform.position = _data._StartPos + _dir * _value;
            EasyInputManager._Instance._Data._Joysticks[_InputIndex] = _dir;
        }

        protected override void OnTouchUp(InputClass _data)
        {
            base.OnTouchUp(_data);
            _JoyStickUiInner.transform.position = _JoyStickUiOuter.transform.position;
            EasyInputManager._Instance._Data._Joysticks[_InputIndex] = Vector3.zero;
        }
    }
}
