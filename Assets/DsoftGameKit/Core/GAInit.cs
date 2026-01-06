using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

namespace DsoftGameKit
{
    public class GAInit : MonoBehaviour
    {
        public static GAInit _Instance;
        private void Awake()
        {
            if (_Instance == null) _Instance = this;
        }

        private void Start()
        {
            GameAnalytics.Initialize();
        }

        public void SendProggresEvent(string _name)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _name);
        }

        public void SendDesighnEvent(string _name)
        {
            GameAnalytics.NewDesignEvent(_name);
        }
    }
}
