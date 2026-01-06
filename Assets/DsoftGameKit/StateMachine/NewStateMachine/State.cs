using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField , Space(10) , Header("ParallelState")]
        State ParallelState;

        //OnEnter----------------------------------------------
        public virtual void OnStart(StateMachine machine)
        {
            ParallelState?.OnStart(machine);
            _FixedMachine = machine;
        }

        //OnUpdate---------------------------------------------
        public virtual State OnStateUpdate(StateMachine machine)
        {
            ParallelState?.OnStateUpdate(machine);
            return this;
        }

        //OnExit----------------------------------------------
        public virtual void OnExit(StateMachine machine)
        {
            _FixedMachine = null;
            ParallelState?.OnExit(machine);
        }

        //OnFixedUpdate---------------------------------------
        private void FixedUpdate()
        {
            if (_FixedMachine) OnStateFixedUpdate(_FixedMachine);
        }
        StateMachine _FixedMachine;
        public virtual void OnStateFixedUpdate(StateMachine machine)
        {
            ParallelState?.OnStateFixedUpdate(machine);
        }
    }
}
