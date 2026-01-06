using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newString", menuName = "DsoftGameKit/String")]
    public class StringVariable : ScriptableObject
    {
        public enum VariableGetTypeEnum
        {
            FromValue,
            FromLocalList,
            FromRandomRange
        }

        public enum IndexTypeEnum
        {
            Index,
            RandomIndex,
            RandomIndexWithPriority
        }

        public string Description;
        public VariableGetTypeEnum Type;
        public string Value;

        public IndexTypeEnum IndexType;
        public IntReference ListIndex;
        public List<StringReference> LocalList;

        public string GetSet
        {
            get
            {
                if (Type == VariableGetTypeEnum.FromValue)
                {
                    return Value;
                }
                else if (Type == VariableGetTypeEnum.FromLocalList) 
                {
                    if(IndexType == IndexTypeEnum.Index)
                    {
                        return LocalList[ListIndex.Value].Value;
                    }
                    else
                    {
                        int _ri = Random.Range(0, LocalList.Count);
                        return LocalList[_ri].Value;
                    }
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Value = value;
            }
        }
    }
}

