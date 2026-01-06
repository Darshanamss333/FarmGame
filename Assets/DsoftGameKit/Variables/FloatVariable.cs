using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newFloat", menuName = "DsoftGameKit/Float")]
    public class FloatVariable : ScriptableObject
    {
        public enum VariableGetTypeEnum
        {
            FromValue,
            FromLocalList,
            FromRandomRange,
            Multiply,
            Add
        }

        public enum IndexTypeEnum
        {
            Index,
            RandomIndex,
        }

        public string Description;
        public VariableGetTypeEnum Type;
        public float Value;

        public IndexTypeEnum IndexType;
        public IntReference ListIndex;
        public List<FloatReference> LocalList;

        public FloatReference Min;
        public FloatReference Max;

        public List<FloatReference> MultiplyList;
        public List<FloatReference> AddList;

        public float GetSet
        {
            get
            {
                float result = 0;
                switch (Type)
                {
                    case VariableGetTypeEnum.FromValue:
                        result = Value;
                        break;

                    case VariableGetTypeEnum.FromLocalList:
                        if (IndexType == IndexTypeEnum.Index)
                        {
                            result = LocalList[Mathf.Clamp(ListIndex.Value, 0, LocalList.Count - 1)].Value;
                        }
                        else
                        {
                            int _ri = Random.Range(0, LocalList.Count);
                            result = LocalList[_ri].Value;
                        }
                        break;

                    case VariableGetTypeEnum.FromRandomRange:
                        result = Random.Range(Min.Value, Max.Value);
                        break;

                    case VariableGetTypeEnum.Multiply:
                        result = Value;
                        for (int i = 0; i < MultiplyList.Count; i++)
                        {
                            result = result * MultiplyList[i].Value;
                        }
                        break;

                    case VariableGetTypeEnum.Add:
                        result = Value;
                        for (int i = 0; i < AddList.Count; i++)
                        {
                            result = result + AddList[i].Value;
                        }
                        break;
                }

                return result;
            }
            set
            {
                Value = value;
            }
        }

        public void Add(float _value)
        {
            Value += _value;
        }
    }
}

