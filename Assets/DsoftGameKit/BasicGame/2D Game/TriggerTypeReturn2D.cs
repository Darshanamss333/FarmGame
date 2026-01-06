using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerTypeReturn2D : MonoBehaviour
    {
        public enum TargetType
        {
            Enemy,
            Player,
        }
        [SerializeField]
        TargetType _TargetType;

        [SerializeField] UnityEvent<GameObject> OnTrggierEnter;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = collision.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    OnTrggierEnter?.Invoke(collision.gameObject);
                }
            }
            else
            {
                IHurtable hurtable = collision.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    OnTrggierEnter?.Invoke(collision.gameObject);
                }
            }
        }

        [SerializeField] UnityEvent<GameObject> OnTriggerExit;
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = collision.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    OnTriggerExit?.Invoke(collision.gameObject);
                }
            }
            else
            {
                IHurtable hurtable = collision.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    OnTriggerExit?.Invoke(collision.gameObject);
                }
            }
        }
    }
}
