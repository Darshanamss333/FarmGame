using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class GestureControllManager : MonoBehaviour
    {
        public static GestureControllManager _Instance;

        private void Awake()
        {
            if (_Instance == null) _Instance = this;
        }

        public UnityAction _OnTap;
        public UnityAction _OnHold;
        public UnityAction _OnSwipeDown;
        public UnityAction _OnSwipeUp;
        public UnityAction _OnSwipeLeft;
        public UnityAction _OnSwipeRight;
        bool _holded;
        float _time;
        bool _valid;
        Vector3 _startPos;
        Vector3 _currentPos;
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject == gameObject)
                {
                    _valid = true;
                    _time = 0;
                    _holded = false;
                    _startPos = new Vector3(Input.mousePosition.x * GlobalAction.ScreenToCanvasRatio.x, Input.mousePosition.y * GlobalAction.ScreenToCanvasRatio.y);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if(_valid)
                {
                    _time += Time.deltaTime;
                    if (_time > 0.2f)
                    {
                        _holded = true;
                        Debug.Log("Hold");
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_valid)
                {
                    _currentPos = new Vector3(Input.mousePosition.x * GlobalAction.ScreenToCanvasRatio.x, Input.mousePosition.y * GlobalAction.ScreenToCanvasRatio.y);
                    float _distance = Vector3.Distance(_startPos, _currentPos);
                    Vector3 _dir = (_currentPos - _startPos).normalized;

                    if (_holded == false)
                    {
                        if(_distance > 5)
                        {

                            if (Mathf.Abs(_dir.x) < 0.5f && _dir.y < 0)
                            {
                                _OnSwipeDown?.Invoke();
                                Debug.Log("down");
                            }
                            else if (Mathf.Abs(_dir.x) < 0.5f && _dir.y > 0)
                            {
                                _OnSwipeUp?.Invoke();
                                Debug.Log("up");
                            }
                            else if (Mathf.Abs(_dir.y) < 0.5f && _dir.x < 0)
                            {
                                _OnSwipeLeft?.Invoke();
                                Debug.Log("left");
                            }
                            else
                            {
                                _OnSwipeRight?.Invoke();
                                Debug.Log("right");
                            }
                        }
                        else
                        {
                            _OnTap?.Invoke();
                            Debug.Log("Tap");
                        }
                    }
                }
            }
        }
    }
}
