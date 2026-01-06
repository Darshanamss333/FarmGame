using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class ResourcesShowUi : MonoBehaviour
    {
        public static ResourcesShowUi _Instance;
        private void Awake()
        {
            if (_Instance == null) _Instance = this;
        }

        public Transform _parent;
        public GameObject _prefab;

        private void Start()
        {
            foreach (var item in AppManager.Data._Pack._List)
            {
                GameObject _new = Instantiate(_prefab);
                _new.transform.SetParent(_parent);
                _new.transform.GetChild(0).GetComponent<Image>().sprite = item._Image;
            }
        }

        private void Update()
        {
            int i = 0;
            foreach (var item in AppManager.Data._Pack._List)
            {
                _parent.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = item._Amount.ToString();
                i++;
            }
        }
    }
}
