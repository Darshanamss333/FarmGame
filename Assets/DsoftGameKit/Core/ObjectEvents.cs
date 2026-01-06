using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class ObjectEvents : MonoBehaviour
    {
        public UnityEvent AwakeCallback;
        private void Awake()
        {
            AwakeCallback.Invoke();
        }

        public UnityEvent OnEnableCallback;
        private void OnEnable()
        {
            OnEnableCallback.Invoke();
        }

        public UnityEvent OnStartCallback;
        private void Start()
        {
            OnStartCallback.Invoke();
        }

        public UnityEvent OnDisableCallback;
        private void OnDisable()
        {
            OnDisableCallback.Invoke();
        }
    }
}