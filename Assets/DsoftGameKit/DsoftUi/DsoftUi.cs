using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public static class DsoftUi
    {
        public static T Create<T>(string _name,GameObject _parent)
        {
            if (_parent == null) _parent = GameObject.Find("Canvas");
            GameObject _new = GameObject.Instantiate(GetPrefab<T>(), _parent.transform);
            T _ui = _new.GetComponent<T>();
            DsoftUiParent _uiParent = _ui as DsoftUiParent;
            _uiParent.Name = _name;
            return _ui;
        }

        static GameObject GetPrefab<T>()
        {
            return Resources.Load(typeof(T).Name) as GameObject;
        }
    }
}
