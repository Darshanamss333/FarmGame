using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newGameObject", menuName = "DsoftGameKit/GameObject")]
    public class GameObjectVariable : ScriptableObject
    {
        public enum VariableGetTypeEnum
        {
            FromValue,
            FromLocalList,
        }

        public enum IndexTypeEnum
        {
            Index,
            RandomIndex,
            RandomIndexWithPriority
        }

        [System.Serializable]
        public class PriortyList
        {
            public GameObjectReference Object;
            public FloatReference Priority;
            [HideInInspector] public float min;
            [HideInInspector] public float max;
        }

        public string Description;
        public VariableGetTypeEnum Type;
        public GameObject Value;


        public IndexTypeEnum IndexType;
        public IntReference ListIndex;
        public List<GameObjectReference> LocalList;
        public List<PriortyList> PriorityList;
        [HideInInspector] public float maxPriortyValue;

        public GameObject GetSet
        {
            get
            {
                if (Type == VariableGetTypeEnum.FromValue)
                {
                    return Value;
                }
                else
                {
                    if (IndexType == IndexTypeEnum.Index)
                    {
                        return LocalList[Mathf.Clamp(ListIndex.Value, 0, LocalList.Count - 1)].Value;
                    }
                    else if(IndexType == IndexTypeEnum.RandomIndex)
                    {
                        int _ri = Random.Range(0, LocalList.Count);
                        return LocalList[_ri].Value;
                    }
                    else
                    {
                        float _ri = Random.Range(0, maxPriortyValue);
                        int _resualt = 0;
                        for (int i = 0; i < PriorityList.Count; i++)
                        {
                            if (_ri > PriorityList[i].min && _ri < PriorityList[i].max)
                            {
                                _resualt = i;
                            }
                        }

                        return PriorityList[_resualt].Object.Value;
                    }
                }
            }
            set
            {
                Value = value;
            }
        }


        public void SetValue(GameObject _value)
        {
            Value = _value;
        }

        public void SetNull()
        {
            Value = null;
        }
    }
}
