using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveSelectedLevelObj : MonoBehaviour
    {
        private void OnEnable()
        {
            UpdateActiveChild();
        }

        [SerializeField] GameObject _obj;
        [SerializeField] int _index;
        void UpdateActiveChild()
        {
            _obj.SetActive(false);
            if(_index == LevelManager._Instance.SelectedLevel) _obj.SetActive(true);
        }
    }
}
