using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class WaitState : State
    {
        [SerializeField]
        FloatReference MaxTime;
        [SerializeField]
        FloatReference RandomTimeAdd;
        [SerializeField]
        State NextState;

        float tang;
        float maxTang;
        public override void OnStart(StateMachine machine)
        {
            tang = 0;
            maxTang = MaxTime.Value + Random.Range(0, RandomTimeAdd.Value);
            base.OnStart(machine);
        }

        public override State OnStateUpdate(StateMachine machine)
        {
            tang = Mathf.Clamp(tang + Time.deltaTime , 0 ,maxTang);

            if(tang >= maxTang)
            {
                if (NextState) machine.CurrentState = NextState;
            }

            return base.OnStateUpdate(machine);
        }
    }
}
