using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class FPSLook : MonoBehaviour
    {
        private void Update()
        {
            Look();
        }


        //Look-------------------------------------
        [SerializeField]
        FloatReference _lookSensivity;
        [SerializeField]
        Vector3Reference _input;
        [SerializeField]
        Transform XLookObject;
        [SerializeField]
        Transform YLookObject;
        void Look()
        {
            XLookObject.localEulerAngles = new Vector3(0, _input.Value.x * _lookSensivity.Value , 0) + XLookObject.localEulerAngles;
            YLookObject.localEulerAngles = new Vector3(-_input.Value.y * _lookSensivity.Value, 0, 0) + YLookObject.localEulerAngles;
        }
    }
}
