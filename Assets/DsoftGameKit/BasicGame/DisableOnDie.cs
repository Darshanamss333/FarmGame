using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class DisableOnDie : MonoBehaviour
    {
        [SerializeField] FloatReference _Delay;
        private void Start()
        {
            GetComponent<BaseCharacter>().OnDieAction += delegate
            {
                if(_Delay.Value > 0)
                {
                    Wait _new = new Wait(_Delay.Value);
                    _new.OnTimeOutAction += () => gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            };
        }
    }
}
