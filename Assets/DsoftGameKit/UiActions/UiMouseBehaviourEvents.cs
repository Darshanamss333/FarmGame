using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class UiMouseBehaviourEvents : MonoBehaviour
    {
        public class InputDataClass
        {
            public Vector3 _StartPos;
            public Vector3 _CurrentPos;
            public Vector3 _DeltaPos;
            public Vector3 _Dir;
        }

        public InputDataClass _InputData = new InputDataClass();
        bool Pressed;
        public virtual void Update()
        {
            if(Pressed == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == gameObject)
                    {
                        Pressed = true;
                        _InputData._StartPos = Input.mousePosition;
                        _InputData._CurrentPos = Input.mousePosition;
                        _InputData._DeltaPos = Input.mousePosition;
                        _InputData._Dir = Vector3.zero;
                        OnDown(_InputData);
                    }
                }
            }
            else
            {
                _InputData._CurrentPos = Input.mousePosition;

                if (Input.GetMouseButton(0))
                {
                    OnHold(_InputData);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    OnUp(_InputData);
                    Pressed = false;
                }

                _InputData._Dir = _InputData._CurrentPos - _InputData._DeltaPos;
                _InputData._DeltaPos = _InputData._CurrentPos;
            }
        }

        public virtual void OnDown(InputDataClass _data)
        {

        }
        public virtual void OnHold(InputDataClass _data)
        {

        }
        public virtual void OnUp(InputDataClass _data)
        {

        }
    }
}
