using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Trigger2D : MonoBehaviour
    {
        [SerializeField] GameObjectReference TargetObject;

        [SerializeField] UnityEvent OnTrggierEnter;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == TargetObject.Value) OnTrggierEnter?.Invoke();
        }

        [SerializeField] UnityEvent OnTriggerExit;
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == TargetObject.Value) OnTriggerExit?.Invoke();
        }
    }
}
