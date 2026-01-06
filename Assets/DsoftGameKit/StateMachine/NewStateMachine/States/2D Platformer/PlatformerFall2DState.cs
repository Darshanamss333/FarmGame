using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class PlatformerFall2DState : State
    {
        public override void OnStart(StateMachine machine)
        {
            Playanimation();
            base.OnStart(machine);
        }

        public override State OnStateUpdate(StateMachine machine)
        {
            GroundCheck();
            TryRun(machine);
            TryIdle(machine);
            FlipCharacter();
            return base.OnStateUpdate(machine);
        }

        public override void OnStateFixedUpdate(StateMachine machine)
        {
            Move();
            base.OnStateFixedUpdate(machine);
        }

        //PlayAnimation------------------------------------------
        [SerializeField , Space]
        Animator anim;
        [SerializeField]
        string _IdleAnimation;
        void Playanimation()
        {
            if (anim && _IdleAnimation != "") anim.Play(_IdleAnimation);
        }

        //GroundCheck--------------------------------------------
        [SerializeField]
        bool Ground;
        [SerializeField]
        float RaycastDistance;
        [SerializeField]
        LayerMask Mask;
        void GroundCheck()
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance, Mask))
            {
                Ground = true;
            }
            else
            {
                Ground = false;
            }
        }

        //Move--------------------------------------------------------------
        [SerializeField]
        Rigidbody2D _RB;
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


        //TryRun-------------------------------------------------
        [Header("Run")]
        [SerializeField]
        FloatReference _HorizontalInput;
        [SerializeField]
        State _RunState;
        void TryRun(StateMachine machine)
        {
            if (Ground && Mathf.Abs(_HorizontalInput.Value) > 0) machine.CurrentState = _RunState;
        }

        //TryIdle--------------------------------------------------------------
        [Header("Idle")]
        [SerializeField]
        State _IdleState;
        void TryIdle(StateMachine machine)
        {
            if (_HorizontalInput.Value == 0 && Ground) machine.CurrentState = _IdleState;
            _RB.velocity = new Vector2(0, _RB.velocity.y);
        }
    }

}
