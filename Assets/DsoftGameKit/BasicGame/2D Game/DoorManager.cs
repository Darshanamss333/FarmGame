using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class DoorManager : MonoBehaviour
    {
        public static DoorManager _Instance;
        private void Awake()
        {
            if(_Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                _Instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        public string DoorName;
    }
}
