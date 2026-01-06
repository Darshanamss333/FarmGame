using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class MoveAddState : State
    {
        [SerializeField]
        GameObjectReference Object;
        [SerializeField]
        Vector3Reference AddValue;
        [SerializeField]
        FloatReference Speed;

        [SerializeField]
        State NextState;

        Vector3 _startPos;
        public override void OnStart(StateMachine machine)
        {
            _startPos = transform.position;
            float _distance = Vector3.Distance(_startPos, _startPos + AddValue.Value);
            Wait _new = new Wait(_distance / Speed.Value);
            _new.OnWaitAction += delegate
            {
                Object.Value.transform.position = Vector3.Lerp(_startPos, _startPos + AddValue.Value, _new.Tang);
            };

            _new.OnTimeOutAction += delegate
            {
                machine.CurrentState = NextState;
            };

            base.OnStart(machine);
        }
    }
}
