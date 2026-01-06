using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class LocalScaleSquashAndStretchAnimation : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += SquashAndStretch;
            _originalScale = transform.localScale;
        }

        [SerializeField] float _Time = 0.3f;
        [SerializeField] float _Value = 0.1f;
        Vector3 _originalScale;
        public void SquashAndStretch()
        {
            Wait _new = new Wait(_Time);
            float x = 0;
            float y = 0;
            _new.OnWaitAction += delegate
            {
                if (_new.Tang < 0.33f)
                {
                    x = Mathf.Lerp(_originalScale.x, _originalScale.x + (_originalScale.x * _Value) , Mathf.InverseLerp(0, 0.33f, _new.Tang));
                    y = Mathf.Lerp(_originalScale.y, _originalScale.y - (_originalScale.y * _Value), Mathf.InverseLerp(0, 0.33f, _new.Tang));
                }
                else
                {
                    if(_new.Tang < 0.66f)
                    {
                        x = Mathf.Lerp(transform.localScale.x, _originalScale.x - (_originalScale.x * _Value), Mathf.InverseLerp(0.33f, 0.66f, _new.Tang));
                        y = Mathf.Lerp(transform.localScale.y, _originalScale.y + (_originalScale.y * _Value), Mathf.InverseLerp(0.33f, 0.66f, _new.Tang));
                    }
                    else
                    {
                        x = Mathf.Lerp(transform.localScale.x, _originalScale.x, Mathf.InverseLerp(0.66f, 1f, _new.Tang));
                        y = Mathf.Lerp(transform.localScale.y, _originalScale.y, Mathf.InverseLerp(0.66f, 1f, _new.Tang));
                    }
                }
                
                transform.localScale = new Vector3(x, y, transform.localScale.z);
            };

            _new.OnTimeOutAction += delegate
            {
                transform.localScale = _originalScale;
            };
        }
    }
}