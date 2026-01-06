using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerLayer2D : MonoBehaviour
    {
        [SerializeField] LayerMask _Mask;
        [SerializeField] UnityEvent OnTrggierEnter;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((_Mask.value & (1 << collision.gameObject.layer)) > 0)
            {
                OnTrggierEnter?.Invoke();
            }
        }

        [SerializeField] UnityEvent OnTriggerExit;
        private void OnTriggerExit2D(Collider2D collision)
        {
            if ((_Mask.value & (1 << collision.gameObject.layer)) > 0)
            {
                OnTriggerExit?.Invoke();
            }
        }
    }
}
