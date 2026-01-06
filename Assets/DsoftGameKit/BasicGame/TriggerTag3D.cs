using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerTag3D : MonoBehaviour
    {
        [SerializeField] StringReference _TagName;
        [SerializeField] UnityEvent _OnTrggierEnter;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _TagName.Value) _OnTrggierEnter?.Invoke();
        }

        [SerializeField] UnityEvent _OnTriggerExit;
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == _TagName.Value) _OnTriggerExit?.Invoke();
        }

    }
}
