using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class UiTouchBehaviourEvents : MonoBehaviour
    {
        public class InputClass
        {
            public Vector3 _StartPos;
            public Vector3 _CurrentPos;
        }
        
        InputClass _Data = new InputClass();
        bool touched = false;
        int id = 1000;
        protected virtual void Update()
        {
            if(Application.isEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == gameObject && touched == false)
                    {
                        touched = true;
                        _Data._StartPos = Input.mousePosition;
                        _Data._CurrentPos = Input.mousePosition;
                        OnTouchDown(_Data);
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    //Debug.Log(Input.mousePosition);
                    _Data._CurrentPos = Input.mousePosition;
                    OnTouchHold(_Data);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    touched = false;
                    _Data._CurrentPos = Input.mousePosition;
                    OnTouchUp(_Data);
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        Touch t = Input.touches[i];

                        if (t.phase == TouchPhase.Began)
                        {
                            if (EventSystem.current.IsPointerOverGameObject(t.fingerId) && EventSystem.current.currentSelectedGameObject == gameObject && touched == false)
                            {
                                id = t.fingerId;
                                touched = true;
                                _Data._StartPos = t.position;
                                _Data._CurrentPos = t.position;
                                OnTouchDown(_Data);
                            }
                        }

                        if (t.phase == TouchPhase.Moved)
                        {
                            if (t.fingerId == id)
                            {
                                _Data._CurrentPos = t.position;
                                OnTouchHold(_Data);
                            }
                        }

                        if (t.phase == TouchPhase.Stationary)
                        {
                            if (t.fingerId == id)
                            {
                                _Data._CurrentPos = t.position;
                                OnTouchHold(_Data);
                            }
                        }

                        if (t.phase == TouchPhase.Ended)
                        {
                            if (t.fingerId == id)
                            {
                                id = 1000;
                                touched = false;
                                _Data._CurrentPos = t.position;
                                OnTouchUp(_Data);
                            }
                        }

                        if (t.phase == TouchPhase.Canceled)
                        {
                            if (t.fingerId == id)
                            {
                                id = 1000;
                                touched = false;
                                _Data._CurrentPos = t.position;
                                OnTouchUp(_Data);
                            }
                        }

                    }
                }

            }
        }

        protected virtual void OnTouchDown(InputClass _data)
        {

        }

        protected virtual void OnTouchHold(InputClass _data)
        {

        }

        protected virtual void OnTouchUp(InputClass _data)
        {

        }
    }
}
