using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class AxisInputState : State
    {
        public string InputName;
        public FloatReference Value;
        public FloatReference Sensitive;
        public FloatReference Gravity;

        public override State OnStateUpdate(StateMachine machine)
        {
            float _delta = Input.GetAxis(InputName);
            if(Mathf.Abs(_delta) > 0) Value.Value = Mathf.Lerp(Value.Value, _delta, Time.deltaTime * Sensitive.Value);
            if (_delta == 0) Value.Value = Mathf.Lerp(Value.Value, _delta, Time.deltaTime * Gravity.Value);
            return base.OnStateUpdate(machine);
        }

        public bool ResetValueWhenExit;
        public override void OnExit(StateMachine machine)
        {
            if (ResetValueWhenExit) Value.Value = 0;
            base.OnExit(machine);
        }
    }
}
