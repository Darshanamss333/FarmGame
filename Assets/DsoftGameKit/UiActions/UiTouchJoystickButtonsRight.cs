using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class UiTouchJoystickButtonsRight : UiTouchBehaviourEvents
    {
        [SerializeField] int _InputIndex;
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
            Vector3 _delta = EasyInputManager._Instance._Data._Joysticks[_InputIndex];
            EasyInputManager._Instance._Data._Joysticks[_InputIndex] = new Vector3(1, _delta.y, _delta.z);
        }


        protected override void OnTouchUp(InputClass _data)
        {
            base.OnTouchUp(_data);
            Vector3 _delta = EasyInputManager._Instance._Data._Joysticks[_InputIndex];
            EasyInputManager._Instance._Data._Joysticks[_InputIndex] = new Vector3(0, _delta.y, _delta.z);
        }
    }
}
