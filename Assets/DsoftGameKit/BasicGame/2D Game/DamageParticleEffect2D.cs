using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class DamageParticleEffect2D : MonoBehaviour
    {
        [Header("Event-----------------------------")] 
        [SerializeField] bool _AwakeOnStart;
        [SerializeField] bool _AwakeOnEnable;
        [SerializeField] bool _AwakeOnDisable;
        [SerializeField] bool _awkeOnDistroy;
        [SerializeField] bool _AwakeOnDamage;
        [SerializeField] bool _AwakeOnDie;
        [SerializeField] bool _AwakeOnTriggerEnter;
        private void Start()
        {
            if (_AwakeOnStart) Particel();
            if(_AwakeOnDamage) GetComponent<BaseCharacter>().OnDamageAction += Particel;
            if(_AwakeOnDie) GetComponent<BaseCharacter>().OnDieAction += Particel;
        }

        private void OnEnable()
        {
            if (_AwakeOnEnable) Particel();
        }

        private void OnDisable()
        {
            if (_AwakeOnDisable) Particel();
        }

        private void OnDestroy()
        {
            if (_awkeOnDistroy) Particel();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_AwakeOnTriggerEnter) Particel();
        }

        [Header("Setting---------------------------")]
        [SerializeField, Range(0, 10)] float _LifeTime;
        [SerializeField, Range(0, 10)] float _ParticleCount;
        [SerializeField, Range(0, 10)] float _VelocityValue;
        [SerializeField, Range(0,10)] float _RotateValue;
        [SerializeField,Range(0,10) ] float _ScaleValue = 1;
        [SerializeField] string _SortingLayer;
        [SerializeField] int _SortingOrder;
        [SerializeField] List<Sprite> _Sprites;

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


                _new.Object.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                Vector3 _randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
                

                _new.OnWaitAction += delegate
                {
                    _new.Object.transform.position = Vector3.Lerp(transform.position, transform.position + _randomDir * _VelocityValue, _new.Tang);
                    _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, Mathf.InverseLerp(2f, 0, _new.Tang));
                    _new.Object.transform.Rotate(0,0,_RotateValue);
                    _new.Object.transform.localScale = Vector3.Lerp(Vector3.one , new Vector3(_ScaleValue,_ScaleValue , 1), _new.Tang);
                };
            }
        }
    }
}

