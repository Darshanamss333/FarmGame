using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class RemoveAds : MonoBehaviour
    {

        public void OpenRemoveAdsWindow()
        {
            RemoveAdsWindowUi._Instance.OpenRemoveAdsWindow();
        }

        private void Update()
        {
            if (GameManager._Instance._Data._RemoveAds)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
