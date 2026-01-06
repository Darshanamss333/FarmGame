using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TriggerTypeReturn3D : MonoBehaviour
    {
        public enum TargetType
        {
            Enemy,
            Player,
        }
        [SerializeField]
        TargetType _TargetType;
        [SerializeField] GameObjectReference _Resault;

        [SerializeField] UnityEvent<GameObject> _OnTrggierEnter;
        private void OnTriggerEnter(Collider other)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = other.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    _OnTrggierEnter?.Invoke(other.gameObject);
                    _Resault.Value = other.gameObject;
                }
            }
            else
            {
                IHurtable hurtable = other.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    _OnTrggierEnter?.Invoke(other.gameObject);
                    _Resault.Value = other.gameObject;
                }
            }
        }


        [SerializeField] UnityEvent<GameObject> _OnTriggerExit;
        private void OnTriggerExit(Collider other)
        {
            if (_TargetType == TargetType.Enemy)
            {
                IDamageble damageble = other.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    _OnTriggerExit?.Invoke(other.gameObject);
                    if (_Resault.Value && _Resault.Value == other.gameObject) _Resault.Value = null;
                }
            }
            else
            {
                IHurtable hurtable = other.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    _OnTriggerExit?.Invoke(other.gameObject);
                    if (_Resault.Value && _Resault.Value == other.gameObject) _Resault.Value = null;
                }
            }
        }
    }
}
