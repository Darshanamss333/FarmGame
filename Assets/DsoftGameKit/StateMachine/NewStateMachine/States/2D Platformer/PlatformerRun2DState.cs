using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class PlatformerRun2DState : State
    {
        public override void OnStateFixedUpdate(StateMachine machine)
        {
            Move();
            base.OnStateFixedUpdate(machine);
        }

        public override State OnStateUpdate(StateMachine machine)
        {
            GroundCheck();
            FlipCharacter();
            TryIdle(machine);
            TryJump(machine);
            return base.OnStateUpdate(machine);
        }

        public override void OnStart(StateMachine machine)
        {
            Playanimation();
            base.OnStart(machine);
        }


        //GroundCheck--------------------------------------------
        [SerializeField]
        Raycast2DState GroundCheckState;
        void GroundCheck()
        {
            GroundCheckState.RaycastCheck();
        }

        //Move--------------------------------------------------------------
        [SerializeField]
        Rigidbody2D _RB;
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


        //PlayAnimation--------------------------------------------------------
        [SerializeField , Space]
        Animator anim;
        [SerializeField]
        string _RunAnimation;
        void Playanimation()
        {
            if (anim && _RunAnimation != "") anim.Play(_RunAnimation);
        }

        //TryIdle--------------------------------------------------------------
        [Header("Idle")]
        [SerializeField]
        State _IdleState;
        void TryIdle(StateMachine machine)
        {
            if (Mathf.Abs(_HorizontalInput.Value) < 0.5f)
            {
                machine.CurrentState = _IdleState;
            }
        }


        //TryJump-----------------------------------------------
        [Header("Jump")]
        [SerializeField]
        FloatReference _JumpInput;
        [SerializeField]
        State _JumpState;
        bool IsButtonReady;
        void TryJump(StateMachine machine)
        {
            if (_JumpInput.Value == 0)
            {
                IsButtonReady = true;
            }
            /*
            if (_JumpInput.Value == 1 && Ground && IsButtonReady)
            {
                machine.CurrentState = _JumpState;
                _deltaGround = Ground;
                IsButtonReady = false;
            }
            */
        }
    }

}
