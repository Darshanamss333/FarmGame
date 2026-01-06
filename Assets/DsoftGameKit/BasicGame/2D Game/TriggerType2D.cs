using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerType2D : MonoBehaviour
    {
        public enum TargetType
        {
            Enemy,
            Player,
        }
        [SerializeField]
        TargetType _TargetType;

        [SerializeField] UnityEvent OnTrggierEnter;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = collision.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    OnTrggierEnter?.Invoke();
                }
            }
            else
            {
                IHurtable hurtable = collision.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    OnTrggierEnter?.Invoke();
                }
            }
        }

        [SerializeField] UnityEvent OnTriggerExit;
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = collision.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    OnTriggerExit?.Invoke();
                }
            }
            else
            {
                IHurtable hurtable = collision.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    OnTriggerExit?.Invoke();
                }
            }
        }
    }
}
