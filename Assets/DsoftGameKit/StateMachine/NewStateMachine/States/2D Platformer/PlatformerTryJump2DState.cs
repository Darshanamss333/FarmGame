using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class PlatformerTryJump2DState : State
    {


        [SerializeField]
        FloatReference _Input;
        public override State OnStateUpdate(StateMachine machine)
        {
            GroundCheck();
            TryJump(machine);
            return base.OnStateUpdate(machine);
        }

        //GroundCheck--------------------------------------------
        [SerializeField]
        bool Ground;
        [SerializeField]
        float RaycastDistance;
        [SerializeField]
        LayerMask Mask;
        bool _deltaGround;
        void GroundCheck()
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance, Mask))
            {
                Ground = true;
                _deltaGround = true;
            }
            else
            {
                if(_deltaGround)
                {
                    Wait _wait = new Wait(0.2f);
                    _wait.OnTimeOutAction += delegate { Ground = false; };
                    _deltaGround = false;
                }

                //Ground = false;
            }
        }

        //TryJump-----------------------------------------------
        [SerializeField]
        State JumpState;
        bool IsButtonReady;
        void TryJump(StateMachine machine)
        {
            if(_Input.Value == 0)
            {
                IsButtonReady = true;
            }

            if (_Input.Value == 1 && Ground && IsButtonReady)
            {
                machine.CurrentState = JumpState;
            }
        }

        public override void OnExit(StateMachine machine)
        {
            _deltaGround = Ground;
            IsButtonReady = false;
            base.OnExit(machine);
        }
    }

}
