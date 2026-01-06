using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.IO;
using System;

namespace DsoftGameKit
{
    public class AppHelper : MonoBehaviour
    {
        public static AppHelper _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public GameObject _Canvas;
        public RectTransform _CanvasRect;
    }
}
