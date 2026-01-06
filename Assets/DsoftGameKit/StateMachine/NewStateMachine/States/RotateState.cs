using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class RotateState : State
    {
        [SerializeField]
        Transform Object;
        [SerializeField]
        Space Space;
        [SerializeField]
        Vector3Reference Rotate;

        public override State OnStateUpdate(StateMachine machine)
        {
            Object.Rotate(Rotate.Value , Space);
            return base.OnStateUpdate(machine);
        }
    }
}
