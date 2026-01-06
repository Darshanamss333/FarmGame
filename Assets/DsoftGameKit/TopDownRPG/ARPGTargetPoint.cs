using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class ARPGTargetPoint : UiTouchBehaviourEvents
    {
        [SerializeField] LayerMask _Mask;
        [SerializeField] Vector3Reference _TargetPos;
        [SerializeField] UnityEvent _OnClick;

        private void OnEnable()
        {
            //OnTouchStart += TouchPoint;
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == this.gameObject)
                {
                    Ray _ray = new Ray(Camera.main.transform.position, (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) - Camera.main.transform.position).normalized);
                    RayPoint(_ray);
                }
            }
        }


        void TouchPoint(Touch _t)
        {
            Ray _ray = new Ray(Camera.main.transform.position, (Camera.main.ScreenToWorldPoint(new Vector3(_t.position.x, _t.position.y, Camera.main.nearClipPlane)) - Camera.main.transform.position).normalized);
            RayPoint(_ray);
        }

        private void RayPoint(Ray _ray)
        {
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit, float.PositiveInfinity, _Mask))
            {
                _TargetPos.Value = _hit.point;
                _OnClick?.Invoke();
            }
        }
    }
}
