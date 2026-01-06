using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class SimpleStateMachine<T>
    {
        SimpleState<T> _currentState;
        SimpleState<T> _deltaState;

        [SerializeField] string _StateName = "";
        public SimpleState<T> CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
                _StateName = value.GetType().Name;
            }
        }

        public void UpdateState(T _machine)
        {

            if(CurrentState != _deltaState)
            {
                if (_deltaState != null) _deltaState.ExitState(_machine);

                CurrentState.EnterState(_machine);
                _deltaState = CurrentState;
            }
            else
            {
                CurrentState = CurrentState.UpdateState(_machine);
            }
        }

        public void FixedUpdateState(T _machine)
        {
            CurrentState.FixedUpdateState(_machine);
        }
    }
}
