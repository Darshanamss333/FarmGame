using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class IntReference
    {
        public bool IsConstant = true;
        public int Constance;
        public IntVariable Variable;

        public int Value
        {
            get
            {
                return IsConstant ? Constance : Variable.Value;
            }
            set
            {
                if(IsConstant)
                {
                    Constance = value;
                }
                else
                {
                    Variable.Value = value;
                }
            }
        }
    }
}
