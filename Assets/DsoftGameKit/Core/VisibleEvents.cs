using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class VisibleEvents : MonoBehaviour
    {
        public UnityEvent VisibleCallback;
        private void OnBecameVisible()
        {
            VisibleCallback.Invoke();
        }

        public UnityEvent InvisibleCallback;
        private void OnBecameInvisible()
        {
            InvisibleCallback.Invoke();
        }
    }
}