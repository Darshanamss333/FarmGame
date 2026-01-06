using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class AutomatedObjects : MonoBehaviour
    {
        protected virtual ResourcesManager.ResType returntype
        {
            get
            {
                return ResourcesManager._Coin;
            }
        }


        int _max = 5;
        ResourcesManager.ResPack _store;

        private void Start()
        {
            _store = new ResourcesManager.ResPack();
        }

        float _tang = 0;
        private void Update()
        {
            if (_tang < 10)
            {
                _tang += Time.deltaTime;
            }
            else
            {
                _store.Get(returntype)._Amount = Mathf.Clamp(_store.Get(returntype)._Amount + 1, 0, _max);
                _tang = 0;
            }

            updateUI();
        }

        GameObject NeedResorcesParent;
        void updateUI()
        {
            if(WindowManager._Instance._CurrentWindow == WindowManager.WindowTypeEnum.GameWindow)
            {
                if (NeedResorcesParent)
                {
                    NeedResorcesParent.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));

                    int i = 0;
                    foreach (var item in _store._List)
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

                    foreach (var item in _store._List)
                    {
                        GameObject _new = Instantiate(ResourcesShowUi._Instance._prefab);
                        _new.transform.SetParent(NeedResorcesParent.transform);
                        _new.transform.GetChild(0).GetComponent<Image>().sprite = item._Image;
                    }
                }
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
                        foreach (var item in _store._List)
                        {
                            AppManager.Data._Pack.Get(item)._Amount += item._Amount;
                            StartCoroutine(IDropRes(item, item._Amount));
                            item._Amount = 0;
                        }
                    }
                };
            }

        }

        IEnumerator IDropRes(ResourcesManager.ResType _type, int count)
        {
            for (int i = 0; i < count; i++)
            {
                ResourcesShow.Drop(_type, transform.position, ResourcesShow.ResourcesUiParentWorldPos);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == AppManager.Farmer.gameObject) _enter = false;
        }
    }
}
