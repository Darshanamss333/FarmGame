using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace DsoftGameKit
{
    public class UnityEventState : State
    {
        [SerializeField]
        UnityEvent OnStartEvent;

        public override void OnStart(StateMachine machine)
        {
            OnStartEvent?.Invoke();
            base.OnStart(machine);
        }

        [SerializeField]
        UnityEvent OnExitEvent;
        public override void OnExit(StateMachine machine)
        {
            OnExitEvent?.Invoke();
            base.OnExit(machine);
        }
    }
}
