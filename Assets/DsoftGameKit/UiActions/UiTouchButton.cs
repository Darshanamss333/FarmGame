using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class UiTouchButton : UiTouchBehaviourEvents
    {
        [SerializeField] int _InputIndex;
        private void OnEnable()
        {
            EasyInputManager._Instance._Data._Buttons[_InputIndex] = 0;
        }

        private void OnDisable()
        {
            EasyInputManager._Instance._Data._Buttons[_InputIndex] = 0;
        }

        protected override void OnTouchDown(InputClass _data)
        {
            base.OnTouchDown(_data);
            EasyInputManager._Instance._Data._Buttons[_InputIndex] = 1;
        }


        protected override void OnTouchUp(InputClass _data)
        {
            base.OnTouchUp(_data);
            EasyInputManager._Instance._Data._Buttons[_InputIndex] = 0;
        }
    }
}
