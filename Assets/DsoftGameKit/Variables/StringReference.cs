using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class StringReference
    {
        public bool IsConstant = true;
        public string Constance;
        public StringVariable Variable;

        public string Value
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
