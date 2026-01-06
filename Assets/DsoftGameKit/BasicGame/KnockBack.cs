using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class KnockBack : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += _KnockBack;
        }


        [SerializeField] Rigidbody _rb;
        [SerializeField] UnityEvent _OnKnockBackFinish;
        public void _KnockBack()
        {
            
            Wait _new = new Wait(0.5f);
            _new.OnWaitAction += delegate
            {
                transform.Translate(0, 0, -50 * Time.deltaTime);
            };

            _new.OnTimeOutAction += () => _OnKnockBackFinish?.Invoke();
            
        }
    }
}
