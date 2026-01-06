using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class CommenEvents : MonoBehaviour
    {
        [SerializeField] bool _OnStart;
        [SerializeField] bool _OnUpdate;
        [SerializeField] bool _OnEnable;
        [SerializeField] bool _OnDisable;
        [SerializeField] bool _OnDamage;
        [SerializeField] bool _onDie;
        [SerializeField] bool _onTrigger2DEnter;
        [SerializeField] bool _onTrigger2DExit;
        [SerializeField] bool _onTrigger2DStay;
        [SerializeField] bool _OnPlayerTrigger2DEnter;
        [SerializeField] bool _OnPlayerTrigger2dExit;

        public UnityAction _OnStarAction;
        public UnityAction _OnUpdateAction;
        public UnityAction _OnEnableAction;
        public UnityAction _OnDisableAction;
        public UnityAction _OnDamageAction;
        public UnityAction _OnDieAction;
        public UnityAction _OnTrigger2DEnterAction;
        public UnityAction _OnTrigger2DExitAction;
        public UnityAction _OnTrigger2DStayAction;
        public UnityAction _OnPlayerTrigger2DEnterAction;
        public UnityAction _OnPlayerTrigger2dExitAction;
        public UnityAction _OnAllAction;

        private void Awake()
        {
            if (_OnStart) _OnStarAction?.Invoke();
            if (_OnDamage)
            {
                if (GetComponent<BaseCharacter>()) GetComponent<BaseCharacter>().OnDamageAction += delegate
                {
                    _OnDamageAction?.Invoke();
                    _OnAllAction?.Invoke();
                };
            }

            if (_onDie)
            {
                if (GetComponent<BaseCharacter>()) GetComponent<BaseCharacter>().OnDieAction += delegate
                {
                    _OnDieAction?.Invoke();
                    _OnAllAction?.Invoke();
                };
            }

            AfterAwake();
        }

        public virtual void AfterAwake()
        {

        }

        private void Update()
        {
            if (_OnUpdate)
            {
                _OnUpdateAction?.Invoke();
                _OnAllAction?.Invoke();
            }
        }

        private void OnEnable()
        {
            if (_OnEnable)
            {
                _OnEnableAction?.Invoke();
                _OnAllAction?.Invoke();
            }
        }

        private void OnDisable()
        {
            if (_OnDisable)
            {
                _OnDisableAction?.Invoke();
                _OnAllAction?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_onTrigger2DEnter)
            {
                _OnTrigger2DEnterAction?.Invoke();
                _OnAllAction?.Invoke();
            }

            if(_OnPlayerTrigger2DEnter)
            {
                IHurtable hurtable = collision.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    _OnPlayerTrigger2DEnterAction?.Invoke();
                    _OnAllAction?.Invoke();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_onTrigger2DExit)
            {
                _OnTrigger2DExitAction?.Invoke();
                _OnAllAction?.Invoke();
            }

            if (_OnPlayerTrigger2dExit)
            {
                IHurtable hurtable = collision.GetComponent<IHurtable>();
                if (hurtable != null)
                {
                    _OnPlayerTrigger2dExitAction?.Invoke();
                    _OnAllAction?.Invoke();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_onTrigger2DStay)
            {
                _OnTrigger2DStayAction?.Invoke();
                _OnAllAction?.Invoke();
            }
        }
    }
}
