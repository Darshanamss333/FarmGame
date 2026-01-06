using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(EventTrigger))]
    public class UiBehaviourEvents : MonoBehaviour
    {
        //Start----------------------------------------------------

        void Start()
        {
            EventTrigger _EventTrigger = gameObject.GetComponent<EventTrigger>();

            EventTrigger.Entry _pointerDown = new EventTrigger.Entry();
            _pointerDown.eventID = EventTriggerType.PointerDown;
            _pointerDown.callback.AddListener(delegate { PointerDown(); });
            _EventTrigger.triggers.Add(_pointerDown);

            EventTrigger.Entry _beginDrag = new EventTrigger.Entry();
            _beginDrag.eventID = EventTriggerType.BeginDrag;
            _beginDrag.callback.AddListener(delegate { BeginDrag(); });
            _EventTrigger.triggers.Add(_beginDrag);

            EventTrigger.Entry _endDrag = new EventTrigger.Entry();
            _endDrag.eventID = EventTriggerType.EndDrag;
            _endDrag.callback.AddListener(delegate { EndDrag(); });
            _EventTrigger.triggers.Add(_endDrag);

            EventTrigger.Entry _pointerUp = new EventTrigger.Entry();
            _pointerUp.eventID = EventTriggerType.PointerUp;
            _pointerUp.callback.AddListener(delegate { PointerUp(); });
            _EventTrigger.triggers.Add(_pointerUp);

            EventTrigger.Entry _pointerExit = new EventTrigger.Entry();
            _pointerExit.eventID = EventTriggerType.PointerExit;
            _pointerExit.callback.AddListener(delegate { PointerExit(); });
            _EventTrigger.triggers.Add(_pointerExit);
        }

        private void Update()
        {
            Drag();
        }

        //PointerUp-------------------------------------------------
        public UnityAction PointerDownAction;
        void PointerDown()
        {
            PointerDownAction?.Invoke();
        }

        //BeginDrag-------------------------------------------------
        Vector3 _deltaMousePos;
        bool _isDragBegin;
        public UnityAction BeginDragAction;
        void BeginDrag()
        {
            BeginDragAction?.Invoke();
            _deltaMousePos = Input.mousePosition;
            _isDragBegin = true;
        }

        //Drag-------------------------------------------------
        public UnityAction<Vector3> DragAction;
        void Drag()
        {
            if (_isDragBegin)
            {
                DragAction?.Invoke(Input.mousePosition - _deltaMousePos);
                _deltaMousePos = Input.mousePosition;
            }
        }

        //EndDrag-------------------------------------------------
        public UnityAction EndDragAction;
        void EndDrag()
        {
            _isDragBegin = false;
            EndDragAction?.Invoke();
        }

        //PointerUp-------------------------------------------------
        public UnityAction PointerUpAction;
        void PointerUp()
        {
            _isDragBegin = false;
            PointerUpAction?.Invoke();
        }

        //PointerExit-------------------------------------------------
        public UnityAction PointerExitAction;
        void PointerExit()
        {
            PointerExitAction?.Invoke();
        }
    }

}