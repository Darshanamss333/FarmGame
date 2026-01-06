using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(BaseCharacter))]
    public class DamageHitEffect2DAdvance : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<BaseCharacter>().OnDamageAction += Effect;
        }

        [SerializeField] List<SpriteRenderer> _renderer;
        void Effect()
        {
            if(!_start)
            {
                for (int i = 0; i < _renderer.Count; i++)
                {
                    _renderer[i].color = Color.red;
                }
                _start = true;
            }

        }

        bool _start;
        float _tang;
        private void Update()
        {
            if(_start)
            {
                _tang += Time.deltaTime;

                if(_tang > 0.2f)
                {
                    _start = false;
                    _tang = 0;

                    for (int i = 0; i < _renderer.Count; i++)
                    {
                        _renderer[i].color = Color.white;
                    }
                }
            }
        }

    }

}
