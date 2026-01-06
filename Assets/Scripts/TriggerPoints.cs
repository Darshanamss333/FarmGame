using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class TriggerPoints : MonoBehaviour
    {
        Tiles _endtile;
        public Tiles EndTile
        {
            get
            {
                return _endtile;
            }
            set
            {
                _endtile = value;
            }
        }

        Tiles _current;
        public Tiles Current
        {
            set
            {
                _current = value;
            }
        }

        bool _enter;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == AppManager.Farmer.gameObject)
            {
                _enter = true;
                Wait _new = new Wait(1);
                _new.OnTimeOutAction += delegate
                {
                    if (_enter)
                    {
                        bool _canunlock = true;
                        foreach (var item in _endtile.needed._List)
                        {
                            int _haveamount = AppManager.Data._Pack.Get(item)._Amount;
                            int _needamount = item._Amount;
                            int _cangive = Mathf.Clamp(_haveamount, 0, _needamount);
                            item._Amount = (int)Mathf.Clamp(item._Amount - _haveamount, 0, Mathf.Infinity);
                            AppManager.Data._Pack.Get(item)._Amount = (int)Mathf.Clamp(AppManager.Data._Pack.Get(item)._Amount - _needamount, 0, Mathf.Infinity);
                            StartCoroutine(IDropRes(item,_cangive));
                            if (item._Amount > 0) _canunlock = false;
                        }
                        if(_canunlock)
                        {
                            _endtile.Showing = true;
                            GAInit._Instance.SendProggresEvent("tile " + _endtile.transform.GetSiblingIndex() + " unlocked");
                            GAInit._Instance.SendDesighnEvent("TileUnloked");
                            AppManager.RefreshAllTiles();
                        }
                    }
                };
            }

        }

        IEnumerator IDropRes(ResourcesManager.ResType _type, int count)
        {
            for (int i = 0; i < count; i++)
            {
                ResourcesShow.Drop(_type, ResourcesShow.ResourcesUiParentWorldPos, AppManager.Farmer.transform.position);
                yield return new WaitForSeconds(0.1f);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == AppManager.Farmer.gameObject) _enter = false;
        }


        GameObject NeedResorcesParent;
        private void Update()
        {
            if(WindowManager._Instance._CurrentWindow == WindowManager.WindowTypeEnum.GameWindow)
            {
                if (NeedResorcesParent)
                {
                    NeedResorcesParent.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));

                    int i = 0;
                    foreach (var item in _endtile.needed._List)
                    {
                        if (item._Amount > 0)
                        {
                            NeedResorcesParent.transform.GetChild(i).gameObject.SetActive(true);
                            NeedResorcesParent.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = item._Amount.ToString();
                        }
                        else
                        {
                            NeedResorcesParent.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        i++;
                    }
                }
                else
                {
                    NeedResorcesParent = Instantiate(ResourcesShowUi._Instance._parent.gameObject);
                    NeedResorcesParent.AddComponent<CanvasGroup>().interactable = false;
                    foreach (Transform item in NeedResorcesParent.transform)
                    {
                        Destroy(item.gameObject);
                    }
                    NeedResorcesParent.transform.SetParent(ResourcesShowUi._Instance.transform);

                    foreach (var item in _endtile.needed._List)
                    {
                        GameObject _new = Instantiate(ResourcesShowUi._Instance._prefab);
                        _new.transform.SetParent(NeedResorcesParent.transform);
                        _new.transform.GetChild(0).GetComponent<Image>().sprite = item._Image;
                    }
                }
            }
        }

        private void OnDisable()
        {
            if (NeedResorcesParent) NeedResorcesParent.SetActive(false);
        }

        private void OnEnable()
        {
            if (NeedResorcesParent) NeedResorcesParent.SetActive(true);
        }
    }
}
