using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveSelectedLevelChild : MonoBehaviour
    {
        private void OnEnable()
        {
            UpdateActiveChild();
        }

        void UpdateActiveChild()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            int _childnumber = (int)Mathf.Repeat(LevelManager._Instance.SelectedLevel, transform.childCount);
            transform.GetChild(_childnumber).gameObject.SetActive(true);
        }
    }
}
