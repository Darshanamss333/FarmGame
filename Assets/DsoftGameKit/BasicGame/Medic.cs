using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;
using UnityEngine.Events;

public class Medic : MonoBehaviour 
{
    [SerializeField] FloatReference _HealValue;
    [SerializeField] UnityEvent OnEnter;
    private void OnTriggerEnter(Collider other)
    {
        IHealable healable = other.GetComponent<IHealable>();
        if (healable != null)
        {
            healable.Heal(_HealValue.Value);
            OnEnter.Invoke();
        }
    }
}
