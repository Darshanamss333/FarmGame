using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class GameObjectReference
    {
        public int Mode;
        //public bool IsConstant = true;
        public GameObject Constance;
        public GameObjectVariable Variable;
        public GameObjectVariableLocal LocalVariable;

        public GameObject Value
        {
            get
            {
                if(Mode == 0)
                {
                    return Constance;
                }
                else if(Mode == 1)
                {
                    return Variable.GetSet;
                }
                else
                {
                    return LocalVariable.Value;
                }
            }
            set
            {
                if (Mode == 0)
                {
                    Constance = value;
                }
                else if (Mode == 1)
                {
                    Variable.GetSet = value;
                }
                else
                {
                    LocalVariable.Value = value;
                }
            }
        }
    }
}
