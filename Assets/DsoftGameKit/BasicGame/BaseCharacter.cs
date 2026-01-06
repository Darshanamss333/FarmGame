using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class BaseCharacter : MonoBehaviour
    {
        public float Health;
        public float MaxHealth;
        public virtual void OnEnable()
        {
            Health = MaxHealth;
            _isAlive = true;
        }

        public virtual void Update()
        {

        }

        //IsAlive-------------------------
        bool _isAlive;
        public bool IsAlive
        {
            get
            {
                return _isAlive;
            }
            set
            {
                _isAlive = value;
            }
        }

        //Die------------------------------
        public UnityEvent OnDieEvent;
        public UnityAction OnDieAction;
        public virtual void OnDie()
        {
            OnDieAction?.Invoke();
            OnDieEvent?.Invoke();
            IsAlive = false;
        }

        public virtual void OnHit()
        {

        }

        //Damage--------------------------
        public UnityEvent OnDamageEvent;
        public UnityAction OnDamageAction;
        public virtual void Damage(float _value)
        {
            if(IsAlive)
            {
                Health = Mathf.Clamp(Health - _value, 0, MaxHealth);

                if (Health <= 0)
                {
                    OnDie();
                }
                else
                {
                    OnHit();
                    OnDamageAction?.Invoke();
                    OnDamageEvent?.Invoke();
                }
                
                //KnockBack();
            }
        }

        //Heal--------------------------------
        public virtual void Heal(float _value)
        {
            if(_isAlive)
            {
                Health = Mathf.Clamp(Health + _value, 0, MaxHealth);
            }
        }

        //KnockBack----------------------------
        void KnockBack()
        {
            Wait _new = new Wait(0.1f);
            _new.OnWaitAction += delegate
            {
                transform.Translate(-0.1f, 0, 0 * Time.deltaTime , Space.World);
            };
        }
    }

}
