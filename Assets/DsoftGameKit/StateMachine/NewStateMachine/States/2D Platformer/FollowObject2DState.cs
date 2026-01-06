using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{

    public class FollowObject2DState : State
    {
        [SerializeField] GameObjectReference Object;
        [SerializeField] GameObjectReference TargetObject;
        [SerializeField] FloatReference MaxDistance;
        [SerializeField] FloatReference Speed;
        [SerializeField] State NextState;

        public override void OnStart(StateMachine machine)
        {
            Vector3 _dir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized;
            _targetPos = TargetObject.Value.transform.position + (_dir * Random.Range(0, MaxDistance.Value));
            base.OnStart(machine);
        }

        Vector3 _targetPos;
        public override State OnStateUpdate(StateMachine machine)
        {
            if (Object.Value != null)
            {
                if (Object.Value.transform.position != _targetPos)
                {
                    Object.Value.transform.position = Vector3.MoveTowards(Object.Value.transform.position, _targetPos, Speed.Value * Time.deltaTime);
                }
                else
                {
                    if (NextState) machine.CurrentState = NextState;
                }

            }

            return base.OnStateUpdate(machine);
        }
    }
}
