using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class MoveLocal : MonoBehaviour
    {
        [SerializeField]
        FloatReference _X;
        [SerializeField]
        FloatReference _Y;
        [SerializeField]
        FloatReference _Z;
        private void Update()
        {
            transform.Translate(new Vector3(_X.Value * Time.deltaTime, _Y.Value * Time.deltaTime, _Z.Value * Time.deltaTime), Space.Self);
        }
    }
}
