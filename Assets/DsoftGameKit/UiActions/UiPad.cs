using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(UiBehaviourEvents))]
    public class UiPad : MonoBehaviour
    {
        UiBehaviourEvents _events;
        private void Start()
        {
            _events = GetComponent<UiBehaviourEvents>();
            _events.DragAction += Drag;
            _events.PointerUpAction += StopDrag;
            _events.EndDragAction += StopDrag;
        }

        [SerializeField]
        Vector3Reference _Output;
        [SerializeField] 
        FloatReference _OutputHorizontal;
        [SerializeField]
        FloatReference _outputVirtical;
        void Drag(Vector3 _value)
        {
            _Output.Value = _value;
            _OutputHorizontal.Value = _value.x;
            _outputVirtical.Value = _value.y;
        }

        void StopDrag()
        {
            _Output.Value = Vector3.zero;
            _outputVirtical.Value = 0;
            _OutputHorizontal.Value = 0;
        }
    }
}
