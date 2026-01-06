using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField]
        float _X;
        [SerializeField]
        float _Y;
        [SerializeField]
        float _Z;
        private void Update()
        {
            transform.Rotate(new Vector3(_X * Time.deltaTime, _Y * Time.deltaTime, _Z * Time.deltaTime), Space.Self);
        }
    }
}
