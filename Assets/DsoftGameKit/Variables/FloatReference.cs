using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [System.Serializable]
    public class FloatReference
    {
        public enum MathTypeEnum
        {
            Random,
            FromList,
            Add,
            Multiply
        }

        public enum IndexTypeEnum
        {
            FromIndex,
            Random
        }

        public int Mode;
        public bool Advanced;
        public MathTypeEnum Type;

        public float FloatValue;
        public FloatVariable FloatObject;

        public SubFloatReference Min;
        public SubFloatReference Max;

        public List<SubFloatReference> FromList;
        public IndexTypeEnum IndexType;
        public SubFloatReference Index;

        public List<SubFloatReference> AddList;

        public List<SubFloatReference> MultiplyList;


        public float Value
        {
            get
            {
                float _resault = 0;

                if(Advanced)
                {
                    switch (Type)
                    {
                        case MathTypeEnum.Random:
                            _resault = Random.Range(Min.Value, Max.Value);
                            break;

                        case MathTypeEnum.FromList:
                            if (IndexType == IndexTypeEnum.FromIndex)
                            {
                                int _ri = (int)Index.Value;
                                _resault = FromList[Mathf.Clamp(_ri, 0, FromList.Count - 1)].Value;
                            }
                            else
                            {
                                int _ri = Random.Range(0, FromList.Count);
                                _resault = FromList[_ri].Value;
                            }
                            break;


                        case MathTypeEnum.Add:
                            if (Mode == 0) _resault = FloatValue;
                            if (Mode == 1) _resault = FloatObject.Value;
                            for (int i = 0; i < AddList.Count; i++)
                            {
                                _resault += AddList[i].Value;
                            }
                            break;


                        case MathTypeEnum.Multiply:
                            if (Mode == 0) _resault = FloatValue;
                            if (Mode == 1) _resault = FloatObject.Value;
                            for (int i = 0; i < MultiplyList.Count; i++)
                            {
                                _resault *= MultiplyList[i].Value; 
                            }
                            break;
                    }
                }
                else
                {
                    if (Mode == 0)
                    {
                        _resault = FloatValue;
                    }
                    else if (Mode == 1)
                    {
                        _resault = FloatObject.GetSet;
                    }
                }

                return _resault;
            }

            set
            {

                if (Mode == 0)
                {
                    FloatValue = value;
                }
                else if (Mode == 1)
                {
                    FloatObject.GetSet = value;
                }
            }
        }
    }
}
