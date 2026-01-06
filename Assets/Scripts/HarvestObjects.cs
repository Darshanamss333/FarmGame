using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class HarvestObjects : Enemy
    {
        [SerializeField] GameObject _stages;

        public override void OnEnable()
        {
            base.OnEnable();
            stageupdate();
        }

        void stageupdate()
        {
            int _max = _stages.transform.childCount - 1;
            int _current = (int)(_max / MaxHealth * Health);

            foreach (Transform item in _stages.transform)
            {
                item.gameObject.SetActive(false);
            }
            _stages.transform.GetChild(_current).gameObject.SetActive(true);
        }

        void Regrow()
        {
            if(Health <= 0)
            {
                Wait _newwait = new Wait(10);
                _newwait.OnTimeOutAction += delegate
                {
                    Vector3 _deltapos = transform.position;
                    Wait _effectwait = new Wait(1);
                    _effectwait.OnWaitAction += delegate
                    {
                        if (_effectwait.Tang < 0.5f)
                        {
                            transform.position = Vector3.Lerp(transform.position , _deltapos + new Vector3(0, -10, 0), _effectwait.Tang);
                        }
                        else
                        {
                            transform.position = Vector3.Lerp(transform.position, _deltapos, _effectwait.Tang);
                        }
                    };

                    Health = MaxHealth;
                    IsAlive = true;
                    stageupdate();
                };
            }
        }

        public override void Damage(float _value)
        {
            base.Damage(_value);
            shakeEffect();
            stageupdate();
            Regrow();
            damagesounplay();
        }

        protected virtual string _hitsound
        {
            get
            {
                return "Hit";
            }
        }

        void damagesounplay()
        {
            SoundManager._Instance.PlaySound(_hitsound);
        }

        void shakeEffect()
        {
            AnimationCurve _rotatecurve = new AnimationCurve();
            _rotatecurve.AddKey(0.1f, 0.5f);
            _rotatecurve.AddKey(1, 0);

            AnimationCurve _scalecurve = new AnimationCurve();
            _scalecurve.AddKey(0.1f, 0.8f);
            _scalecurve.AddKey(1, 1);

            Quaternion _deltarotation = transform.rotation;
            Wait _new = new Wait(0.2f);
            _new.OnWaitAction += delegate
            {
                float _angle = _rotatecurve.Evaluate(_new.Tang);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.EulerAngles(_angle, 0, 0), Time.deltaTime);
                transform.localScale = Vector3.one * _scalecurve.Evaluate(_new.Tang);
            };

            _new.OnTimeOutAction += delegate
            {
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;
                addresorces();
            };
        }

        protected virtual void addresorces()
        {

        }
    }

}
