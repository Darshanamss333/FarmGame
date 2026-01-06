using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class AnimationEvent : MonoBehaviour
    {
        [System.Serializable]
        public class Event
        {
            public string _Name;
            public UnityEvent _Event;
        }

        public List<Event> _Events;
        public void _AnimationEvent(string _value)
        {
            for (int i = 0; i < _Events.Count; i++)
            {
                if(_value == _Events[i]._Name)
                {
                    _Events[i]._Event?.Invoke();
                }
            }
        }
    }
}
