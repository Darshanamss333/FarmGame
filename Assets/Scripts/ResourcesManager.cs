using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class ResourcesManager : MonoBehaviour
    {
        public static ResType _Coin
        {
            get
            {
                return AppManager.Data._Pack.Get(new Coin());
            }
        }

        public static ResType _Wood
        {
            get
            {
                return AppManager.Data._Pack.Get(new Wood());
            }
        }

        public static ResType _Stone
        {
            get
            {
                return AppManager.Data._Pack.Get(new Stone());
            }
        }

        public static ResType _Water
        {
            get
            {
                return AppManager.Data._Pack.Get(new Water());
            }
        }

        [System.Serializable]
        public class ResPack
        {
            public List<ResourcesManager.ResType> _List;

            public ResPack()
            {
                _List = new List<ResourcesManager.ResType>();
                _List.Add(new ResourcesManager.Coin());
                _List.Add(new ResourcesManager.Wood());
                _List.Add(new ResourcesManager.Stone());
                _List.Add(new ResourcesManager.Water());
            }

            public ResType Get(ResType _type)
            {
                foreach (ResType item in _List)
                {
                    if (item.GetType() == _type.GetType()) return item;
                }
                return null;
            }
        }

        [System.Serializable]
        public class ResType
        {
            public UnityAction _OnchangeAmmount;
            [SerializeField] int _amount;

            public int _Amount
            {
                get
                {
                    return _amount;
                }
                set
                {
                    _amount = value;
                    _OnchangeAmmount?.Invoke();
                }
            }

            public Sprite _Image
            {
                get
                {
                    return Resources.Load<Sprite>(this.GetType().Name);
                }
            }

        }

        [System.Serializable]
        public class Coin : ResType
        {

        }

        [System.Serializable]
        public class Wood : ResType
        {

        }

        [System.Serializable]
        public class Stone : ResType
        {

        }

        [System.Serializable]
        public class Water : ResType
        {

        }
    }
}
