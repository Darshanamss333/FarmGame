using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ObjectAppearScaleEffectOffset : MonoBehaviour
    {
        float _delayMax;
        Vector3 _originalScale;
        private void Awake()
        {
            _delayMax = (transform.GetSiblingIndex() + 1) * 0.1f;
            _originalScale = transform.localScale;
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            Wait _delayWait = new Wait(_delayMax);
            _delayWait.OnTimeOutAction += delegate
            {
                Wait _new = new Wait(0.5f);
                _new.OnWaitAction += delegate
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, _originalScale, _new.Tang);
                };
            };
        }
    }
}
