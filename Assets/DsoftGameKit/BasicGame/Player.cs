using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Player : BaseCharacter, IHurtable ,IHealable
    {
        bool _hurtavalable = true;
        void IHurtable.Hurt(float _value)
        {
            if (_hurtavalable)
            {
                base.Damage(_value);
                _hurtavalable = false;
                _tang = 0;
                CameraShake._instance.MiniShake();
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            SpriteRenderer[] _list = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer item in _list)
            {
                if (item.transform.parent == transform)
                {
                    sprites.Add(item);
                }
            }
        }

        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        float _tang;
        int _deltanum;
        public override void Update()
        {
            base.Update();
            if (_hurtavalable == false && IsAlive)
            {
                _tang += Time.deltaTime;

                if (_tang > 3)
                {
                    col(1);
                    _hurtavalable = true;
                }
                else
                {
                    int _num = (int)(_tang * 10);
                    if (_deltanum != _num)
                    {
                        if (_num % 2 == 0)
                        {
                            col(0);
                        }
                        else
                        {
                            col(1);
                        }
                        _deltanum = _num;
                    }
                }

                void col(float _value)
                {
                    foreach (var item in sprites)
                    {
                        item.color = new Color(item.color.r, item.color.g, item.color.b, _value);
                    }
                }
            }
        }

        //Heal-------------------------
        void IHealable.Heal(float _value)
        {
            base.Heal(_value);
        }


        public static Player _Instance;
        private void Awake()
        {
            if (_Instance == null) _Instance = this;
        }
    }
}
