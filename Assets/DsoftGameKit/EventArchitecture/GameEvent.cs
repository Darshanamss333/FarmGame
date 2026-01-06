using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newGameEvent", menuName = "DsoftGameKit/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        List<GameEventListener> _EventListeners = new List<GameEventListener>();


        public void Raise()
        {
            for(int i = _EventListeners.Count -1; i >=0; i--)
            {
                _EventListeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if(!_EventListeners.Contains(listener))
            {
                _EventListeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (_EventListeners.Contains(listener))
            {
                _EventListeners.Remove(listener);
            }
        }
    }

}

