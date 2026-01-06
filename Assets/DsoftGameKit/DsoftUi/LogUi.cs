using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class LogUi : DsoftUiParent
    {
        public override string Name 
        {
            set
            {
                if(_Instance == null)
                {
                    _Instance = this;
                    Add(value);
                }
                else
                {
                    _Instance.Add(value);
                    Destroy(gameObject);
                }
            }
        }


        public static LogUi _Instance;
        [SerializeField] GameObject _prefab;
        [SerializeField] GameObject _content;
        [SerializeField] GameObject _parent;
        public void Add(string _name)
        {
            /*
            GameObject _new = Instantiate(_prefab, _content.transform);
            _new.transform.GetChild(0).GetComponent<Text>().text = _name;
            Wait _newWait = new Wait(1);
            _newWait.OnTimeOutAction += delegate
            {
                Destroy(_new);
                Refresh();
            };
            Refresh();
            */
        }

        void Refresh()
        {
            for (int i = 0; i < 5; i++)
            {
                _parent.SetActive(false);
                _parent.SetActive(true);
            }
        }
    }
}
