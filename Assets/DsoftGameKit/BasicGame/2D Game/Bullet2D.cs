using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Bullet2D : MonoBehaviour
    {
        public enum TargetType
        {
            Damageble,
            Hurtable,
        }
        [SerializeField]
        GameObjectReference _BulletObject;
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
            _BulletObject.Value.transform.gameObject.SetActive(false);
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
        private void Update()
        {
            _BulletObject.Value.transform.Translate(0, 0, _Speed.Value * Time.deltaTime, Space.Self);

            _destroyTime += Time.deltaTime;

            if (_destroyTime > _maxDestroyTime.Value)
            {
                Destroy();
            }
        }
    }
}