using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerAny2D : MonoBehaviour
    {
        [SerializeField] UnityEvent OnTrggierEnter;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTrggierEnter?.Invoke();
        }

        [SerializeField] UnityEvent OnTriggerExit;
        private void OnTriggerExit2D(Collider2D collision)
        {
            OnTriggerExit?.Invoke();
        }
    }
}
