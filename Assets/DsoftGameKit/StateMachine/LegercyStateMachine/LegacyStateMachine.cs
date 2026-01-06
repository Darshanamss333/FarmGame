using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class LegacyStateMachine : MonoBehaviour
    {
        private void Start()
        {
            if (CurrentState) _CallState(CurrentState);
        }

        [SerializeField]
        LegacyState CurrentState;
        public void _CallState(LegacyState _state)
        {
            if(CurrentState != _state) CurrentState.StateExit();
            _state.StateEnter(this);
            CurrentState = _state;
        }
    }
}
