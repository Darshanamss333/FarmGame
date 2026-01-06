using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class Vector3Reference
    {
        public int Type = 0;

        public Vector3 Constance;
        public Vector3Variable Variable;

        public FloatReference X;
        public FloatReference Y;
        public FloatReference Z;

        public Vector3 Value
        {
            get
            {
                if (Type == 0)
                {
                    return Constance;
                }
                else if (Type == 1 && Variable)
                {
                    return Variable.Value;
                }
                else
                {
                    return new Vector3(X.Value, Y.Value, Z.Value);
                }
            }
            set
            {
                if(Type == 0)
                {
                    Constance = value;
                }
                else if(Type == 1)
                {
                    Variable.Value = value;
                }
            }
        }
    }
}
