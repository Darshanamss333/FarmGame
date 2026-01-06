using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class PlatformerTryMovement2DState : State
    {
        [SerializeField]
        FloatReference _Input;
        [SerializeField]
        State _RunState;
        public override State OnStateUpdate(StateMachine machine)
        {
            if (Mathf.Abs(_Input.Value) > 0) machine.CurrentState = _RunState;
            return base.OnStateUpdate(machine);
        }
    }

}
