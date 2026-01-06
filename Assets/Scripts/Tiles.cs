using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class Tiles : MonoBehaviour
    {
        [System.Serializable]
        class dataclass
        {
            public bool _Active;
            public dataclass(bool _active)
            {
                _Active = _active;
            }
        }
        public bool StartTile;
        [SerializeField] dataclass _data;
        [SerializeField] List<Tiles> _list;
        public ResourcesManager.ResPack needed;

        private void Start()
        {
            Init();
        }

        void Init()
        {
            string _id = "Tile" + transform.position.ToString();
            GameManager._Instance._OnBeforeSave += delegate
            {
                AppManager.Data.SetOtherSlot(_id, JsonUtility.ToJson(_data));
            };

            string _datatext = AppManager.Data.GetOtherSlot(_id);
            if (_datatext == null) _datatext = JsonUtility.ToJson(new dataclass(StartTile));
            _data = JsonUtility.FromJson<dataclass>(_datatext);

            needed = new ResourcesManager.ResPack();
            needed.Get(new ResourcesManager.Wood())._Amount = (int)Mathf.Clamp(transform.position.magnitude, 10, Mathf.Infinity);
            if(transform.position.magnitude > 20) needed.Get(new ResourcesManager.Coin())._Amount = (int)Mathf.Clamp(transform.position.magnitude, 10, Mathf.Infinity);
            if (transform.position.magnitude > 30) needed.Get(new ResourcesManager.Stone())._Amount = (int)Mathf.Clamp(transform.position.magnitude, 20, Mathf.Infinity);

            _list = AppManager.GetNaigberTile(this);

            for (int i = 0; i < MyAllTriggers.Count; i++)
            {
                //MyAllTriggers[i].gameObject.SetActive((_list[i]) ? true : false);
                MyAllTriggers[i].EndTile = _list[i];
                MyAllTriggers[i].Current = this;
                //ResourcesShowUi.NeedResorcesShow(_list[i], MyAllTriggers[i].transform);
            }

            Showing = _data._Active;
            AppManager.RefreshAllTiles();
        }

        public bool Showing
        {
            get
            {
                return _data._Active;
            }
            set
            {
                if (value)
                {
                    Child.transform.localScale = Vector3.zero;
                    CreateScaleEffect();
                }
                Child.SetActive(value);
                _data._Active = value;
            }
        }

        GameObject Child
        {
            get
            {
                return transform.Find("Child").gameObject;
            }
        }

        GameObject TriggersParent
        {
            get
            {
                return Child.transform.Find("TriggersParent").gameObject;
            }
        }

        List<TriggerPoints> MyAllTriggers
        {
            get
            {
                List<TriggerPoints> _res = new List<TriggerPoints>();
                foreach (Transform item in TriggersParent.transform)
                {
                    _res.Add(item.GetComponent<TriggerPoints>());
                }
                return _res;
            }
        }

        List<TriggerPoints> MyAvailbleTriggers
        {
            get
            {
                List<TriggerPoints> _res = new List<TriggerPoints>();
                for (int i = 0; i < _list.Count; i++)
                {
                    if (_list[i]) _res.Add(MyAllTriggers[i]);
                }
                return _res;
            }
        }

        TriggerPoints GetNextTrigger
        {
            get
            {
                foreach (var item in MyAvailbleTriggers)
                {
                    if (item.EndTile.Showing == false) return item;
                }
                return null;
            }
        }

        public void Refresh()
        {
            foreach (var item in MyAllTriggers)
            {
                item.gameObject.SetActive(false);
            }

            GetNextTrigger?.gameObject.SetActive(true);
        }

        void CreateScaleEffect()
        {
            Vector3 _deltapos = Child.transform.localPosition;
            Wait _new = new Wait(0.2f);
            _new.OnWaitAction += delegate
            {
                Child.transform.localScale = Vector3.one * _new.Tang;
                Child.transform.localPosition = Vector3.Lerp(_deltapos + new Vector3(0, -5, 0), _deltapos, _new.Tang);
            };
        }
    }

}
