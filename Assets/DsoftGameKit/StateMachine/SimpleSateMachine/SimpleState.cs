using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public abstract class SimpleState<T>
    {
        public virtual void EnterState(T _machine)
        {

        }

        public virtual SimpleState<T> UpdateState(T _machine)
        {
            return this;
        }

        public virtual void ExitState(T _machine)
        {

        }

        public virtual void FixedUpdateState(T _machine)
        {

        }
    }
}
