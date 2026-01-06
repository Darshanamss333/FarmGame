using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{

    public class ParticleSystem2D : MonoBehaviour
    {
        [Header("Event-----------------------------")]
        public bool _AwakeOnStart;
        public bool _AwakeOnEnable;
        public bool _AwakeOnDisable;
        public bool _AwkeOnDistroy;
        public bool _AwakeOnDamage;
        public bool _AwakeOnDie;
        public bool _AwakeOnTriggerEnter2D;
        public bool _AwakeOnTriggerExit2D;


        private void Start()
        {
            if (_AwakeOnStart) StartEmitor();
            if (_AwakeOnDamage) GetComponent<BaseCharacter>().OnDamageAction += StartEmitor;
            if (_AwakeOnDie) GetComponent<BaseCharacter>().OnDieAction += StartEmitor;
        }

        private void OnEnable()
        {
            if (_AwakeOnEnable) StartEmitor();
        }

        private void OnDisable()
        {
            if (_AwakeOnDisable) Particel();
        }

        private void OnDestroy()
        {
            if (_AwkeOnDistroy) Particel();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_AwakeOnTriggerEnter2D) StartEmitor();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_AwakeOnTriggerExit2D) StartEmitor();
        }

        [Header("LoopSettings---------------------------")]
        public bool Loop;
        public int MaxRepeatCount = 1;
        public float RepeatInterval = 1;

        [Header("DelaySettings--------------------------")]
        public float Delay;

        [Header("LifeTimeSettings-----------------------")]
        public float _LifeTime = 1;

        [Header("ParticleCountSettings------------------")]
        public float _ParticleCount = 1;

        [Header("VolumeSettings-------------------------")]
        public float _Volume;

        [Header("VelocitySettings-----------------------")]
        public bool _ReturenVelocityMode;
        public float _StartVelocity;
        public bool _StartRandomVelocity;
        public Vector2 _StartRandomVelocityValues;
        public AnimationCurve _VelocityCurveOverLifeTime;

        [Header("DirectionSettings-----------------------")]
        public Vector2 _StartDirectionValue;
        public bool _StartRandomDirection;

        [Header("RotationSettings------------------------")]
        [Range(0,360)]
        public float _StartRotationValue;
        public bool _RandomStartRotation;
        public float _RotateOverLifeTimeValue;
        public AnimationCurve _RotationCurveOverLifeTime;


        [Header("ScaleSettings---------------------------")]
        public float _StartScaleValue = 1;
        public bool _RandomStartScale;
        public Vector2 _RandomStartScaleValue = Vector3.one;
        public AnimationCurve _ScaleCurveOverLifeTime;

        [Header("SortingSettings-------------------------")]
        public string _SortingLayer;
        public int _SortingOrder;

        [Header("SpritesSettings-------------------- ----")]
        public List<Sprite> _Sprites;

        public void Particel()
        {

            for (int i = 0; i < _ParticleCount; i++)
            {
                Wait _new = new Wait(_LifeTime);
                _new.Object.transform.position = transform.position;
                SpriteRenderer _renderer = _new.Object.AddComponent<SpriteRenderer>();
                _renderer.sprite = _Sprites[Random.Range(0, _Sprites.Count)];
                _renderer.sortingLayerName = _SortingLayer;
                _renderer.sortingOrder = _SortingOrder;

                //Volume--------------
                Vector3 _pos = transform.position + new Vector3(Random.Range(-_Volume, _Volume), Random.Range(-_Volume, _Volume), 0);
                _new.Object.transform.position = _pos;

                //Velocity------------
                float _startVelo = _StartVelocity;
                if (_StartRandomVelocity) _startVelo = Random.Range(_StartRandomVelocityValues.x, _StartRandomVelocityValues.y);


                //Direction------------
                Vector3 _dir = _StartDirectionValue;
                if(_StartRandomDirection) _dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

                //Rotation-------------
                float _rot = _StartRotationValue;
                if (_RandomStartRotation) _rot = Random.Range(0, 360);
                _new.Object.transform.rotation = Quaternion.Euler(0, 0, _rot);

                //Scale-----------------
                float _scale = _StartScaleValue;
                if (_RandomStartScale) _scale = Random.Range(_RandomStartScaleValue.x, _RandomStartScaleValue.y);
                _new.Object.transform.localScale = Vector3.zero;


                _new.OnWaitAction += delegate
                {
                    //Velocity
                    float _velocityCurve = _VelocityCurveOverLifeTime.Evaluate(_new.Tang) * _startVelo;
                    if (_ReturenVelocityMode) _new.Object.transform.position = _pos + (_dir * _velocityCurve);
                    if (!_ReturenVelocityMode) _new.Object.transform.Translate(_dir * _velocityCurve);

                    //Color
                    _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, Mathf.InverseLerp(2f, 0, _new.Tang));

                    //Rotation
                    float _rotationCurve = _RotationCurveOverLifeTime.Evaluate(_new.Tang) * _RotateOverLifeTimeValue;
                    _new.Object.transform.Rotate(0, 0, _rotationCurve);

                    //Scale
                    float _scaleCurve = _ScaleCurveOverLifeTime.Evaluate(_new.Tang) * _scale;
                    _new.Object.transform.localScale = new Vector3(_scaleCurve, _scaleCurve, 1);
                };
            }

        }

        //StartEmitor---------------
        bool IsStart;
        void StartEmitor()
        {
            IsStart = true;
        }

        //StopEmitor---------------
        void StopEmiter()
        {
            IsStart = false;
            delayTang = 0;
            currentRepeatCount = 0;
        }


        float delayTang;
        float intervalTang;
        int currentRepeatCount;
        private void Update()
        {
            if (IsStart)
            {
                if (delayTang != Delay)
                {
                    delayTang = Mathf.Clamp(delayTang + Time.deltaTime, 0, Delay);
                }
                else
                {
                    if (Loop)
                    {
                        if (intervalTang == 0)
                        {
                            Particel();
                            currentRepeatCount += 1;

                            if (currentRepeatCount == MaxRepeatCount)
                            {
                                StopEmiter();
                            }
                        }

                        intervalTang = Mathf.Clamp(intervalTang + Time.deltaTime, 0, RepeatInterval);

                        if (intervalTang == RepeatInterval)
                        {
                            intervalTang = 0;
                        }
                    }
                    else
                    {
                        Particel();
                        StopEmiter();
                    }
                }
            }
        }
    }
}
