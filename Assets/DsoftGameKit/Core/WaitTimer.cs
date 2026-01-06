using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class WaitTimer : MonoBehaviour
    {
        public UnityAction OnTimeOutAction;
        [SerializeField] UnityEvent OnTimeOutEvent;
        public UnityAction OnWaitAction;
        public float _currentTime;
        public float _maxTime;
        private void Update()
        {
            if (_currentTime < _maxTime)
            {
                _currentTime += Time.deltaTime;
                OnWaitAction?.Invoke();
            }
            else
            {
                OnTimeOutAction?.Invoke();
                OnTimeOutEvent?.Invoke();
                Destroy(gameObject);
            }
        }
    }

}
