using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class SimpleAnimationEvent : MonoBehaviour
    {
        public string _Name;
        public UnityEvent _Event;
        public void _AnimationEvent(string _value)
        {
            if (_value == _Name)
            {
                _Event?.Invoke();
            }
        }
    }
}
