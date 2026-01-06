using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class PlatformerTryIdle2DState : State
    {
        [SerializeField]
        FloatReference _Input;
        [SerializeField]
        State _IdleState;
        public override State OnStateUpdate(StateMachine machine)
        {
            if (Mathf.Abs(_Input.Value) == 0) machine.CurrentState = _IdleState;
            return base.OnStateUpdate(machine);
        }
    }

}
