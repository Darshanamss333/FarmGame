using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class AiForceJump2DState : State
    {
        [SerializeField] Rigidbody2D RB;
        [SerializeField] GameObjectReference Target;
        [SerializeField] FloatReference JumpValue;
        [Range(0, 10) ,SerializeField] float RandomJumpValue;
        [SerializeField] State NextState;
        public override void OnStart(StateMachine machine)
        {
            float _xDir = Target.Value.transform.position.x - transform.position.x;
            if(_xDir > 0)
            {
                _xDir = 1;
            }
            if (_xDir < 0)
            {
                _xDir = -1;
            }

            Vector2 _dir = new Vector2(_xDir, 1);
            RB.AddForce(_dir * (JumpValue.Value + Random.Range(0, RandomJumpValue)), ForceMode2D.Impulse);

            if (NextState) machine.CurrentState = NextState;

            base.OnStart(machine);
        }
    }
}
