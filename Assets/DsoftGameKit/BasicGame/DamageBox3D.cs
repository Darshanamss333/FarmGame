using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class DamageBox3D : MonoBehaviour
    {
        [SerializeField] FloatReference _damageValue;
        [SerializeField] UnityEvent _OnHit;
        private void OnTriggerEnter(Collider other)
        {
            IDamageble damageble = other.gameObject.GetComponent<IDamageble>();
            if (damageble != null)
            {
                damageble.Damage(_damageValue.Value);
                _OnHit?.Invoke();
            }
        }
    }
}
