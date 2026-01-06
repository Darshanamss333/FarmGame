using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class iTweenShakePosition : MonoBehaviour
    {
        [SerializeField]
        GameObject Target;
        [SerializeField]
        FloatReference X;
        [SerializeField]
        FloatReference Y;
        [SerializeField]
        FloatReference Z;
        [SerializeField]
        FloatReference Time;
        public void _ShakeObject()
        {
            iTween.ShakePosition(Target, new Vector3(X.Value, Y.Value, Z.Value), Time.Value);
        }
    }
}
