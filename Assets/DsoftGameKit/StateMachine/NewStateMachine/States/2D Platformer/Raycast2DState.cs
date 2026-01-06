using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class Raycast2DState : State
    {
        public override State OnStateUpdate(StateMachine machine)
        {
            RaycastCheck();
            return base.OnStateUpdate(machine);
        }

        //GroundCheck--------------------------------------------
        [SerializeField]
        public bool Hit;
        [SerializeField]
        float RaycastDistance;
        [SerializeField]
        LayerMask Mask;
        public void RaycastCheck()
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance, Mask))
            {
                Hit = true;
            }
            else
            {
                Hit = false;
            }
        }
    }

}
