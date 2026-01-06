using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class ForLoop : MonoBehaviour
    {
        public UnityEvent OnLoopEvent;
        public UnityEvent OnLoopEndEvent;
        public void _StartEvent(float _MaxLoop)
        {
            for(int i = 0; i < _MaxLoop; i++)
            {
                OnLoopEvent.Invoke();
            }

            OnLoopEndEvent.Invoke();
        }
    }
}
