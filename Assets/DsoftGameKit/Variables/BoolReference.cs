using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class BoolReference
    {
        public bool IsConstant = true;
        public bool Constance;
        public BoolVariable Variable;

        public bool Value
        {
            get
            {
                if(IsConstant)
                {
                    return Constance;
                }
                else
                {
                    return Variable.GetSet;
                }
            }
            set
            {
                if(IsConstant)
                {
                    Constance = value;
                }
                else
                {
                    Variable.GetSet = value;
                }
            }
        }
    }
}
