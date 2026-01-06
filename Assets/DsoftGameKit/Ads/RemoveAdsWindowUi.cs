using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class RemoveAdsWindowUi : MonoBehaviour
    {
        public static RemoveAdsWindowUi _Instance
        {
            get
            {
                if (RemoveAdsWindowUi.LocalInstance == null)
                {
                    GameObject _canvas = GameObject.Find("Canvas").gameObject;
                    GameObject _new = GameObject.Instantiate(Resources.Load("RemoveAdsWindowUi") as GameObject, _canvas.transform);
                    RemoveAdsWindowUi _Instance = _new.GetComponent<RemoveAdsWindowUi>();
                    RemoveAdsWindowUi.LocalInstance = _Instance;
                    return _Instance;
                }
                else
                {
                    return RemoveAdsWindowUi.LocalInstance;
                }

            }
        }

        public static RemoveAdsWindowUi LocalInstance;
        [SerializeField] GameObject _window;

        public void RemoveAds()
        {
            GameManager._Instance._Data._RemoveAds = true;
            Close();
        }

        private void Update()
        {
            if (GameManager._Instance._Data._RemoveAds)
            {
                _window.SetActive(false);
            }
        }

        public void OpenRemoveAdsWindow()
        {
            _window.SetActive(true);
        }

        public void Close()
        {
            _window.SetActive(false);
        }
    }

}
