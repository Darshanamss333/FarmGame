using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class PlatformerJump2DState : State
    {
        public override void OnStart(StateMachine machine)
        {
            _RB.velocity = new Vector2(_RB.velocity.x, 0);
            _StartPos = transform.position;
            _Jumping = false;
            _deltaJumpSpeed = _JumpSpeed.Value;
            base.OnStart(machine);
        }

        public override void OnStateFixedUpdate(StateMachine machine)
        {
            //TryFall(machine);
            Jump(machine);
            Move();
            base.OnStateFixedUpdate(machine);
        }

        public override State OnStateUpdate(StateMachine machine)
        {
            TryFall(machine);
            FlipCharacter();
            return base.OnStateUpdate(machine);
        }

        //Jump------------------------------------------------------------
        [SerializeField]
        Rigidbody2D _RB;
        [SerializeField]
        FloatReference _JumpInput;
        [SerializeField]
        FloatReference _JumpSpeed;
        float _deltaJumpSpeed;
        [SerializeField]
        FloatReference _MaxHight;
        [SerializeField]
        FloatReference _MinHight;
        void Jump(StateMachine machine)
        {
            _RB.velocity = new Vector2(_RB.velocity.x, _deltaJumpSpeed);
            _deltaJumpSpeed = Mathf.Lerp(_deltaJumpSpeed, _deltaJumpSpeed - 0.5f, Time.deltaTime);
        }

        //Move--------------------------------------------------------------
        [SerializeField]
        FloatReference _HorizontalInput;
        [SerializeField]
        FloatReference _MoveSpeed;
        void Move()
        {
            _RB.velocity = new Vector2(_HorizontalInput.Value * _MoveSpeed.Value, _RB.velocity.y);
        }

        //FlipCharacter-------------------------------------------------------
        void FlipCharacter()
        {
            if (_HorizontalInput.Value > 0) _RB.gameObject.transform.localScale = new Vector3(1, _RB.gameObject.transform.localScale.y, _RB.gameObject.transform.localScale.z);
            if (_HorizontalInput.Value < 0) _RB.gameObject.transform.localScale = new Vector3(-1, _RB.gameObject.transform.localScale.y, _RB.gameObject.transform.localScale.z);
        }

        //TryFall--------------------------------------------------------------
        [Header("Fall")]
        [SerializeField]
        State FallState;
        Vector2 _StartPos;
        bool _Jumping;
        void TryFall(StateMachine machine)
        {
            if (!_Jumping && _RB.velocity.y > 0)
            {
                _Jumping = true;
            }

            if (_RB.velocity.y <= 0f && _Jumping)
            {
                machine.CurrentState = FallState;
            }
            
            if (_JumpInput.Value == 0)
            {
                if (Vector2.Distance(_StartPos, transform.position) >= _MinHight.Value)
                {
                    //_RB.velocity = new Vector2(_RB.velocity.x, 0);
                    machine.CurrentState = FallState;
                }
            }
            

            if (Vector2.Distance(_StartPos, transform.position) >= _MaxHight.Value)
            {
                machine.CurrentState = FallState;
            }
        }
    }

    

}
