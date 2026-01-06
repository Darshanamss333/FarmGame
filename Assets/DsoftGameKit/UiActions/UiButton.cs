using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(UiBehaviourEvents))]
    public class UiButton : MonoBehaviour
    {
        UiBehaviourEvents _events;
        private void Start()
        {
            _events = GetComponent<UiBehaviourEvents>();
            _events.PointerDownAction += ButtonDown;
            _events.PointerUpAction += ButtonUp;
        }

        [SerializeField]
        UnityEvent OnButtonDown;
        void ButtonDown()
        {
            OnButtonDown?.Invoke();
        }

        [SerializeField]
        UnityEvent OnButtonUp;
        void ButtonUp()
        {
            OnButtonUp?.Invoke();
        }
    }
}
