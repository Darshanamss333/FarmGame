using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class SubFloatReference
    {
        public bool Type = true;

        public float Constance;
        public FloatVariable Variable;

        public float Value
        {
            get
            {
                if(Type)
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
                if(Type)
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
