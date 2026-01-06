using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SetCameraRoom2D : MonoBehaviour
    {
        private void OnEnable()
        {
            ClampCameraFollow2D._Instance.Room = gameObject;
        }
    }

}
