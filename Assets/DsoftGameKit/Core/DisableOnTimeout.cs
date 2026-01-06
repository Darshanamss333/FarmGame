using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class DisableOnTimeout : MonoBehaviour
    {
        private void OnEnable()
        {
            _tang = 0;
        }

        public float _Time;
        float _tang;
        private void Update()
        {
            if (_Time > _tang)
            {
                _tang += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
