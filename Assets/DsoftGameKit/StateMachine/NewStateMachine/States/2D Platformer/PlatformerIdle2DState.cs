using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class PlatformerIdle2DState : State
    {
        public override void OnStart(StateMachine machine)
        {
            Playanimation();
            Stop();
            base.OnStart(machine);
        }

        public override State OnStateUpdate(StateMachine machine)
        {
            TryRun(machine);
            TryJump(machine);
            GroundCheck();
            return base.OnStateUpdate(machine);
        }

        //Stop---------------------------------------------------
        [SerializeField]
        Rigidbody2D RB;
        void Stop()
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
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

        //GroundCheck----------------------------------------------
        [SerializeField]
        Raycast2DState GroundCheckState;
        void GroundCheck()
        {
            GroundCheckState.RaycastCheck();
        }

        //TryRun-------------------------------------------------
        [Header("Run")]
        [SerializeField]
        FloatReference _HorizontalInput;
        [SerializeField]
        State _RunState;
        void TryRun(StateMachine machine)
        {
            if (GroundCheckState.Hit && Mathf.Abs(_HorizontalInput.Value) > 0.5f)
            {
                machine.CurrentState = _RunState;
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

            if (_JumpInput.Value == 1 && GroundCheckState.Hit && IsButtonReady)
            {
                machine.CurrentState = _JumpState;
                IsButtonReady = false;
            }
        }
    }

}
