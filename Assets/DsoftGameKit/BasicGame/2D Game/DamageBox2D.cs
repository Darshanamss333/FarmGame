using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class DamageBox2D : MonoBehaviour
    {
        [SerializeField] FloatReference _damageValue;
        [SerializeField] UnityEvent _OnHit;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
            if (damageble != null)
            {
                damageble.Damage(_damageValue.Value);
                _OnHit?.Invoke();
            }
        }
    }
}
