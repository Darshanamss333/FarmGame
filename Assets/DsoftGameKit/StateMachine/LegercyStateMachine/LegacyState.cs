using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class LegacyState : MonoBehaviour
    {
        [SerializeField]
        LegacyState NextState;
        LegacyStateMachine _stateMachine;

        public UnityEvent OnStateEnter;
        public void StateEnter(LegacyStateMachine _machine)
        {
            _stateMachine = _machine;
            OnStateEnter.Invoke();
        }

        public UnityEvent OnStateExit;
        public void StateExit()
        {
            OnStateExit.Invoke();
        }

        public void _CallNextState()
        {
            _stateMachine._CallState(NextState);
        }
    }
}
