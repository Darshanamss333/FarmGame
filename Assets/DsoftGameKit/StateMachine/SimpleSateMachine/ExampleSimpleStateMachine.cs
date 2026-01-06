using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ExampleSimpleStateMachine : MonoBehaviour
    {
        public SimpleState<ExampleSimpleStateMachine> _Idle = new Idle();
        public SimpleState<ExampleSimpleStateMachine> _Walk = new Walk();
        public SimpleStateMachine<ExampleSimpleStateMachine> _StateMachine = new SimpleStateMachine<ExampleSimpleStateMachine>();

        private void OnEnable()
        {
            _StateMachine.CurrentState = _Idle;
        }

        private void Update()
        {
            _StateMachine.UpdateState(this);
        }
    }

    public class Idle : SimpleState<ExampleSimpleStateMachine>
    {
        public override void EnterState(ExampleSimpleStateMachine _machine)
        {
            Debug.Log("Idle");
            Wait _new = new Wait(1);
            _new.OnTimeOutAction += () => _machine._StateMachine.CurrentState = _machine._Walk;
        }
    }

    public class Walk : SimpleState<ExampleSimpleStateMachine>
    {
        public override void EnterState(ExampleSimpleStateMachine _machine)
        {
            Debug.Log("Walk");
            Wait _new = new Wait(1);
            _new.OnTimeOutAction += () => _machine._StateMachine.CurrentState = _machine._Idle;
        }
    }
}
