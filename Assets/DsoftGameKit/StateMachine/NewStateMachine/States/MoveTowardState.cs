using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class MoveTowardState : State
    {
        [SerializeField]
        GameObjectReference Object;
        [SerializeField]
        Vector3Reference Position;
        [SerializeField]
        FloatReference Speed;

        [SerializeField]
        State NextState;

        public override State OnStateUpdate(StateMachine machine)
        {
            if(Object.Value != null)
            {
                if (Object.Value.transform.position != Position.Value)
                {
                    Object.Value.transform.position = Vector3.MoveTowards(Object.Value.transform.position, Position.Value, Speed.Value * Time.deltaTime);
                }
                else
                {
                    if(NextState) machine.CurrentState = NextState;
                }

            }

            return base.OnStateUpdate(machine);
        }
    }
}
