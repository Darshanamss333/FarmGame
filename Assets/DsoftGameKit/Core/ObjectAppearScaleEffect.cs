using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ObjectAppearScaleEffect : MonoBehaviour
    {
        Vector3 _originalScale;
        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            Wait _new = new Wait(0.5f);
            _new.OnWaitAction += delegate
            {
                transform.localScale = Vector3.Lerp(transform.localScale, _originalScale, _new.Tang);
            };
        }
    }
}
