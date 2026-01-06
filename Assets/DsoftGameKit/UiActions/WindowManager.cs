using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class WindowManager : MonoBehaviour
    {
        public enum WindowTypeEnum
        {
            None,
            GameTitleWindow,
            GameWindow,
            GameOverWindow,
        }

        [System.Serializable]
        public class WindowDataClass
        {
            public WindowTypeEnum _Type;
            public List<GameObject> _WindowObject;

            public WindowDataClass()
            {
                _WindowObject = new List<GameObject>();
            }
        }

        public static WindowManager _Instance;
        private void Awake()
        {
            if (_Instance == null) _Instance = this;
        }

        public List<WindowDataClass> _List;
        public WindowTypeEnum _CurrentWindow = WindowTypeEnum.GameWindow;
        WindowTypeEnum _deltaWindow = WindowTypeEnum.None;

        private void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            if(_CurrentWindow != _deltaWindow)
            {
                foreach (var windowitem in _List)
                {
                    foreach (var objitem in windowitem._WindowObject)
                    {
                        if(windowitem._Type == _CurrentWindow)
                        {
                            objitem.SetActive(true);
                        }
                        else
                        {
                            objitem.SetActive(false);
                        }
                    }
                }
                _deltaWindow = _CurrentWindow;
            }
        }
    }

}
