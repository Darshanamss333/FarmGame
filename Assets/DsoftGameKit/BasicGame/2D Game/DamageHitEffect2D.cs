using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(BaseCharacter))]
    public class DamageHitEffect2D : MonoBehaviour
    {
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            GetComponent<BaseCharacter>().OnDamageAction += Effect;
        }

        SpriteRenderer _renderer;
        void Effect()
        {
            _renderer.color = Color.red;
            Wait _new = new Wait(0.2f);
            _new.OnTimeOutAction += () => _renderer.color = Color.white;
        }
    }

}
