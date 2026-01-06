using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake _instance;

        private void Awake()
        {
            if(!_instance)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _originalZ = transform.localPosition.z;
        }

        [SerializeField] AnimationCurve _XCurve;
        [SerializeField] AnimationCurve _YCurve;
        [SerializeField] float _MiniShakeValue = 0.05f;
        [SerializeField] float _MiniShakeTime = 0.05f;

        public void MiniShake()
        {
            Wait _new = new Wait(_MiniShakeTime);
            _new.OnWaitAction += delegate
            {
                transform.localPosition = new Vector3(_MiniShakeValue * _XCurve.Evaluate(_new.Tang), _MiniShakeValue * _YCurve.Evaluate(_new.Tang), 0);
            };

            _new.OnTimeOutAction += delegate
            {
                transform.localPosition = Vector3.zero;
            };
        }

        [SerializeField] float _SmallShakeValue = 0.05f;
        [SerializeField] float _SmallShakeTime = 0.05f;
        public void SmallShake()
        {
            Wait _new = new Wait(_SmallShakeTime);
            _new.OnWaitAction += delegate
            {
                transform.localPosition = new Vector3(_SmallShakeValue * _XCurve.Evaluate(_new.Tang), _SmallShakeValue * _YCurve.Evaluate(_new.Tang), 0);
            };

            _new.OnTimeOutAction += delegate
            {
                transform.localPosition = Vector3.zero;
            };
        }


        //ZSmallPunch-----------------------------------------------------
        float _originalZ;
        public void ZSmallPunch()
        {
            float _deltaZ = 0;
            Wait _new = new Wait(0.09f);
            _new.OnWaitAction += delegate
            {
                float _time = _new.Tang;
                if(_time < 0.5f)
                {
                    _deltaZ = Mathf.Lerp(transform.localPosition.z, _originalZ + 0.2f, _time);
                }
                else
                {
                    _deltaZ = Mathf.Lerp(transform.localPosition.z, _originalZ, _time);
                }

                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _deltaZ);
            };
        }
    }
}