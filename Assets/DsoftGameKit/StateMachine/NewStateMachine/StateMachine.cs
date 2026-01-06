using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class StateMachine : MonoBehaviour
    {
        public State CurrentState;
        State DeltaCurrentState;


        State _deltaStartState;
        private void Start()
        {
            _deltaStartState = CurrentState;
            UpdateState();
        }

        [SerializeField] bool ResetStateOnEnable;
        private void OnEnable()
        {
            if(ResetStateOnEnable && _deltaStartState)
            {
                DeltaCurrentState = CurrentState;
                CurrentState = _deltaStartState;
            }
        }

        private void Update()
        {
            UpdateState();
        }

        void UpdateState()
        {
            if (DeltaCurrentState != CurrentState)
            {
                DeltaCurrentState?.OnExit(this);
                CurrentState?.OnStart(this);
                DeltaCurrentState = CurrentState;
            }
            else
            {
                CurrentState?.OnStateUpdate(this);
            }
        }
    }
}
