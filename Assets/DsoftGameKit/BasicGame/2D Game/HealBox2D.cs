using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class HealBox2D : MonoBehaviour
    {
        [SerializeField] FloatReference _healValue;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IHealable healable = collision.gameObject.GetComponent<IHealable>();
            if (healable != null)
            {
                healable.Heal(_healValue.Value);
            }
        }
    }
}
