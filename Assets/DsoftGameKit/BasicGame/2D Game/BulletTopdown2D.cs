using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BulletTopdown2D : MonoBehaviour
    {
        public enum TargetType
        {
            Damageble,
            Hurtable,
        }

        [SerializeField]
        TargetType _TargetType;
        [SerializeField]
        FloatReference _DamageValue;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_TargetType == TargetType.Damageble)
            {
                IDamageble damageble = other.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    damageble.Damage(_DamageValue.Value);
                    Destroy();
                }
            }
            else
            {
                IHurtable hurtable = other.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    hurtable.Hurt(_DamageValue.Value);
                    Destroy();
                }
            }
        }

        private void OnBecameInvisible()
        {
            Destroy();
        }

        void Destroy()
        {
            transform.gameObject.SetActive(false);
        }

        [SerializeField]
        FloatReference _maxDestroyTime;
        float _destroyTime;
        private void OnEnable()
        {
            _destroyTime = 0;
        }

        [SerializeField]
        FloatReference _Speed;
        [HideInInspector]
        public Vector3 _direction;
        private void Update()
        {
            transform.Translate(_direction * _Speed.Value * Time.deltaTime, Space.Self);

            _destroyTime += Time.deltaTime;

            if (_destroyTime > _maxDestroyTime.Value)
            {
                Destroy();
            }
        }
    }
}