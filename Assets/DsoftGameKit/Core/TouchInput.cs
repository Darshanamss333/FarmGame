using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class TouchInput : MonoBehaviour
    {
        [SerializeField]
        GameObject _targetUI;
        [SerializeField]
        Vector3Reference _StartPos;
        [SerializeField]
        Vector3Reference _Dir;
        [SerializeField]
        Vector3Reference _EndPos;
        bool isTouching;
        int id = 1000;
        public UnityEvent OnTouchStart;
        public UnityEvent OnTouchEnd;
        private void Update()
        {
            if(Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch t = Input.touches[i];

                    if(t.phase == TouchPhase.Began)
                    {
                        if (EventSystem.current.IsPointerOverGameObject(t.fingerId) && EventSystem.current.currentSelectedGameObject == _targetUI && !isTouching)
                        {
                            _StartPos.Value = t.position;
                            OnTouchStart.Invoke();
                            id = t.fingerId;
                            isTouching = true;
                        }
                    }

                    if(t.phase == TouchPhase.Moved)
                    {
                        if(t.fingerId == id)
                        {
                            _Dir.Value = (new Vector3(t.position.x, t.position.y, 0) - _StartPos.Value).normalized;
                        }
                    }

                    if(t.phase == TouchPhase.Ended)
                    {
                        if(t.fingerId == id)
                        {
                            _EndPos.Value = t.position; 
                            OnTouchEnd.Invoke();
                            id = 1000;
                            isTouching = false;
                        }
                    }
                }
            }
        }
    }
}
