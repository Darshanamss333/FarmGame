using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class HurtBox2D : MonoBehaviour
    {
        [SerializeField] FloatReference _damageValue;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IHurtable hurtable = collision.gameObject.GetComponent<IHurtable>();
            if (hurtable != null)
            {
                hurtable.Hurt(_damageValue.Value);
            }
        }
    }
}
