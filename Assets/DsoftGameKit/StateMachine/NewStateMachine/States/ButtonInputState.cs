using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ButtonInputState : State
    {
        public string InputName;

        [SerializeField]
        State ButtonDownState;
        [SerializeField]
        State ButtonUpState;

        public override State OnStateUpdate(StateMachine machine)
        {
            if(Input.GetButtonDown(InputName))
            {
                if (ButtonDownState) machine.CurrentState = ButtonDownState;
            }

            if (Input.GetButtonUp(InputName))
            {
                if (ButtonUpState) machine.CurrentState = ButtonUpState;
            }

            return base.OnStateUpdate(machine);
        }
    }
}
