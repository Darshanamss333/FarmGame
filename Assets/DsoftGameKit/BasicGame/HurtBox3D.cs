using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class HurtBox3D : MonoBehaviour
    {
        [SerializeField] FloatReference _damageValue;
        private void OnTriggerEnter(Collider other)
        {
            IHurtable hurtable = other.gameObject.GetComponent<IHurtable>();
            if (hurtable != null)
            {
                hurtable.Hurt(_damageValue.Value);
            }
        }
    }
}
