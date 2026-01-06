using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerType3D : MonoBehaviour
    {
        public enum TargetType
        {
            Enemy,
            Player,
        }
        [SerializeField]
        TargetType _TargetType;

        [SerializeField] UnityEvent _OnTrggierEnter;
        private void OnTriggerEnter(Collider other)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = other.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    _OnTrggierEnter?.Invoke();
                }
            }
            else
            {
                IHurtable hurtable = other.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    _OnTrggierEnter?.Invoke();
                }
            }
        }

        [SerializeField] UnityEvent _OnTriggerExit;
        private void OnTriggerExit(Collider other)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = other.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    _OnTriggerExit?.Invoke();
                }
            }
            else
            {
                IHurtable hurtable = other.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    _OnTriggerExit?.Invoke();
                }
            }
        }

    }
}
