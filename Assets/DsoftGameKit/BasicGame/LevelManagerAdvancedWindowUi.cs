using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


namespace DsoftGameKit
{
    public class LevelManagerAdvancedWindowUi : MonoBehaviour , IControlInput 
    {
        [SerializeField] Camera _cam;
        [SerializeField] SpriteRenderer _backgroundSpriteRenderr;
        [SerializeField] Button _panButtonUi;

        private void Start()
        {
            _camPos = _backgroundSpriteRenderr.transform.position;
            _camPos.z = 10;
            Clear();
            RefreshUi();
        }

        private void Update()
        {
            /*
            if(WindowManager._Instance._CurrentWindow == WindowManager.WindowTypeEnum.LevelSelectWindow)
            {
                float _camYsize = _cam.orthographicSize;
                float _camXsize = _camYsize / Screen.height * Screen.width;
                float _spWorldYsize = _backgroundSpriteRenderr.sprite.texture.height / _backgroundSpriteRenderr.sprite.pixelsPerUnit * 0.5f;
                float _spWorldXsize = _backgroundSpriteRenderr.sprite.texture.width / _backgroundSpriteRenderr.sprite.pixelsPerUnit * 0.5f;

                if(_spWorldXsize < _camXsize)
                {
                    _cam.orthographicSize -= 0.1f;
                    SetPos();
                }

                Vector3 _max = _backgroundSpriteRenderr.transform.position + new Vector3(_spWorldXsize, _spWorldYsize , 0);
                Vector3 _min = _backgroundSpriteRenderr.transform.position - new Vector3(_spWorldXsize, _spWorldYsize , 0);
                _cam.transform.position = new Vector3(Mathf.Clamp(_camPos.x, _min.x + _camXsize, _max.x - _camXsize), Mathf.Clamp(_camPos.y, _min.y + _camYsize, _max.y - _camYsize), -10);
            }*/
        }

        Vector3 _camPos;
        public void ControlInput(ControlInput.InputClass _inputs)
        {
            if (_inputs.GetFist()._Event == DsoftGameKit.ControlInput.InputEventEnum.OnHold)
            {
                if (EventSystem.current.currentSelectedGameObject != null)// && EventSystem.current.currentSelectedGameObject == _panButtonUi)
                {
                    Vector3 _current = _cam.ScreenToWorldPoint(_inputs.GetFist()._CurrentPos);
                    Vector3 _delta = _cam.ScreenToWorldPoint(_inputs.GetFist()._DeltaPos);
                    Vector3 _dir = _current - _delta;
                    _camPos -= _dir;
                    SetPos();
                }
            }
        }

        void Clear()
        {
            foreach (Transform item in _UiContentParent.transform)
            {
                Destroy(item.gameObject);
            }

            for (int i = 0; i < _levelPointsParent.transform.childCount; i++)
            {
                GameObject _new = Instantiate(_prefab, _UiContentParent.transform);
            }

            SetPos();
        }

        void SetPos()
        {
            for (int i = 0; i < _UiContentParent.transform.childCount; i++)
            {
                GameObject _uibutton = _UiContentParent.transform.GetChild(i).gameObject;
                GameObject _refObj = _levelPointsParent.transform.GetChild(i).gameObject;
                _uibutton.transform.position = Camera.main.WorldToScreenPoint(_refObj.transform.position);
            }
        }


        [SerializeField] GameObject _levelPointsParent;
        [SerializeField] GameObject _prefab;
        [SerializeField] GameObject _UiContentParent;

        void RefreshUi()
        {
            for (int i = 0; i < _levelPointsParent.transform.childCount; i++)
            {
                GameObject item = _levelPointsParent.transform.GetChild(i).gameObject;
                GameObject _new = _UiContentParent.transform.GetChild(i).gameObject;
                //GameObject _new = Instantiate(_prefab, _UiContentParent.transform);
                _new.transform.position = Camera.main.WorldToScreenPoint(item.transform.position);
                Button _button = _new.GetComponent<Button>();
                GameObject _lock = _new.transform.Find("Lock").gameObject;
                Text _number = _new.transform.Find("Number").gameObject.GetComponent<Text>();
                GameObject _stars = _new.transform.Find("Stars").gameObject;
                GameObject _starsSlots = _new.transform.Find("StarsSlots").gameObject;

                if (LevelManager._Instance._Data.Get(i)._Unlocked)
                {
                    _lock.SetActive(false);
                    _number.gameObject.SetActive(true);
                    _stars.gameObject.SetActive(true);
                    _starsSlots.gameObject.SetActive(true);
                    

                    int _index = i;
                    _button.onClick.RemoveAllListeners();
                    _button.onClick.AddListener(delegate
                    {
                        LevelManager._Instance.SelectedLevel = _index;
                        SceneManager.LoadScene("Levels");
                    });

                    for (int si = 0; si < 3; si++)
                    {
                        if (LevelManager._Instance._Data.Get(i)._Stars > si)
                        {
                            _stars.transform.GetChild(si).gameObject.SetActive(true);
                        }
                        else
                        {
                            _stars.transform.GetChild(si).gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    _lock.SetActive(true);
                    _number.gameObject.SetActive(false);
                    _stars.gameObject.SetActive(false);
                    _starsSlots.gameObject.SetActive(false);
                }

                _number.text = (i + 1).ToString();
            }

            //UnloadAnimatio();
        }

        /*
        void UnloadAnimatio()
        {
            int index = 0;

            if(index <= LevelManager._Instance.SelectedLevel)
            {
                GameObject _child = _UiContentParent.transform.GetChild(Mathf.Clamp(index, 0, _UiContentParent.transform.childCount)).gameObject;

                if(LevelManager._Instance._Data.Get("LvUnlock" + index) == null | LevelManager._Instance._Data.Get("LvUnlock" + index) == "")
                {
                    Wait _big = new Wait(0.5f);
                    _big.OnWaitAction += delegate
                    {
                        _child.transform.localScale = Vector3.Lerp(_child.transform.localScale, Vector3.one * 2, _big.Tang);
                    };

                    _big.OnTimeOutAction += delegate
                    {
                        Wait _small = new Wait(0.5f);
                        _small.OnWaitAction += delegate
                        {
                            _child.transform.localScale = Vector3.Lerp(_child.transform.localScale, Vector3.one, _small.Tang);
                        };

                        _small.OnTimeOutAction += delegate
                        {
                            LevelManager._Instance._Data.Set("LvUnlock" + index , true.ToString());
                            RefreshUi();
                        };
                    };
                }
            }
        }
        */
    }

}
