using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Trigger3D : MonoBehaviour
    {
        [SerializeField] GameObjectReference  _TargetObject;

        [SerializeField] UnityEvent _OnTrggierEnter;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _TargetObject.Value) _OnTrggierEnter?.Invoke();
        }

        [SerializeField] UnityEvent _OnTriggerExit;
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _TargetObject.Value) _OnTriggerExit?.Invoke();

        }
    }
}
