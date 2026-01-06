using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class FPSLookClamp : MonoBehaviour
    {
        private void Update()
        {
            ClampLook();
        }


        //ClampLook--------------------------------
        [SerializeField]
        Transform LookObject;
        [SerializeField]
        float _maxYAngle = 45;
        [SerializeField]
        float _maxXAngle = 45;
        [SerializeField]
        float _clamSmooth = 30;
        void ClampLook()
        {
            if (LookObject.localEulerAngles.x < 360 - _maxXAngle && LookObject.localEulerAngles.x > 180)
            {
                LookObject.localEulerAngles = Vector3.Lerp(LookObject.localEulerAngles, new Vector3(360 - _maxXAngle, LookObject.localEulerAngles.y, LookObject.localEulerAngles.z), Time.deltaTime * _clamSmooth);
            }

            if (LookObject.localEulerAngles.x > _maxXAngle && LookObject.localEulerAngles.x < 180)
            {
                LookObject.localEulerAngles = Vector3.Lerp(LookObject.localEulerAngles, new Vector3(_maxXAngle, LookObject.localEulerAngles.y, LookObject.localEulerAngles.z), Time.deltaTime * _clamSmooth);
            }


            if (LookObject.localEulerAngles.y < 360 - _maxYAngle && LookObject.localEulerAngles.y > 180)
            {
                LookObject.localEulerAngles = Vector3.Lerp(LookObject.localEulerAngles, new Vector3(LookObject.localEulerAngles.x, 360 - _maxYAngle, LookObject.localEulerAngles.z), Time.deltaTime * _clamSmooth);
            }

            if (LookObject.localEulerAngles.y > _maxYAngle && LookObject.localEulerAngles.y < 180)
            {
                LookObject.localEulerAngles = Vector3.Lerp(LookObject.localEulerAngles, new Vector3(LookObject.localEulerAngles.x, _maxYAngle, LookObject.localEulerAngles.z), Time.deltaTime * _clamSmooth);
            }
        }
    }

}
