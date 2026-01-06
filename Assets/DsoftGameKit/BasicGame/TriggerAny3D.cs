using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerAny3D : MonoBehaviour
    {
        [SerializeField] UnityEvent _OnTrggierEnter;
        private void OnTriggerEnter(Collider other)
        {
            _OnTrggierEnter?.Invoke();
        }

        [SerializeField] UnityEvent _OnTriggerExit;
        private void OnTriggerExit(Collider other)
        {
            _OnTriggerExit?.Invoke();
        }

    }
}
