using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TowerDefenseEnemy2D : Enemy ,ITowerDefenseTarget2D
    {
        [SerializeField] FloatReference _Speed;
        [SerializeField] FloatReference _AttackPower;
        [SerializeField] GameObjectReference _PathObject;
        [SerializeField] GameObject _Target;
        [SerializeField] UnityEvent _OnTravelFinish;
        List<Vector3> _Path;
        Travel2D _Travel;

        public GameObject GetTarget()
        {
            return _Target;
        }
        public void SetTarget(GameObject _value)
        {
            _Target = _value;
        }

        Animator _anim;
        private void Start()
        {
            _anim = GetComponent<Animator>();
        }
       

        private void Update()
        {
            switch (_State)
            {
                case States.Attack:
                    Attack();
                    break;
                case States.Walk:
                    Walk();
                    break;
            }


            if(_Target && _Target.active == false)
            {
                _Target = null;
            }
        }

        void Attack()
        {
            if (_DeltaState != _State)
            {
                _anim.Play("Attack");
                _DeltaState = _State;
            }

            if (!_Target | (_Target && _Target.active == false))
            {
                _State = States.Walk;
            }
        }

        public void AttackEvent()
        {
            if (_Target)
            {
                IHurtable _hurt = _Target.GetComponent<IHurtable>();
                if (_hurt != null)
                {
                    _hurt.Hurt(_AttackPower.Value);
                }
            }
        }

        void Walk()
        {
            if (_DeltaState != _State)
            {
                _anim.Play("Run");

                if(_Travel == null)
                {
                    _Path = new List<Vector3>();
                    _Path = _PathObject.Value.GetComponent<Path2D>().GetRandomPath(transform);
                    _Travel = new Travel2D(transform, _Path, _Speed.Value);
                    _Travel.OnCompleteAction += _OnTravelFinish.Invoke;
                }
                else
                {
                    _Travel.Play();
                }

                _DeltaState = _State;
            }

            if(_Target && _Target.active)
            {
                _Travel.Pause();
                _State = States.Attack;
            }
        }


        public enum States
        {
            None, Attack, Walk
        }
        public States _State;
        public States _DeltaState;
    }
}
