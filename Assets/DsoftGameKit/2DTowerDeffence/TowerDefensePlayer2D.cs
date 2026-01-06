using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TowerDefensePlayer2D : Player
    {
        Animator _anim;
        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _OriginalPos = transform.position;
        }

        [SerializeField] FloatReference _Speed;
        [SerializeField] FloatReference _AttackPower;
        [SerializeField] GameObject _Target;
        Vector3 _OriginalPos;
        public GameObject Target
        {
            get
            {
                return _Target;
            }
            set
            {
                _Target = value;
            }
        }

        private void Update()
        {
            switch (_State)
            {
                case States.Idle:
                    Idle();
                    break;
                case States.FollowEnemy:
                    FollowEnemy();
                    break;
                case States.Attack:
                    Attack();
                    break;
                case States.BackToOriginalPos:
                    BackToOriginalPos();
                    break;
            }

        }

        void Idle()
        {
            if(_DeltaState != _State)
            {
                _anim.Play("Idle");
                _DeltaState = _State;
            }

            if (_Target && _Target.active)
            {
                _State = States.FollowEnemy;
            }
        }

        void FollowEnemy()
        {
            if (_DeltaState != _State)
            {
                _anim.Play("Run");

                if (Vector2.Distance(transform.position,_Target.transform.position) > 1)
                {
                    List<Vector3> _path = new List<Vector3>();
                    _path.Add(transform.position);
                    _path.Add(_Target.transform.position + _Target.transform.right * 0.5f);
                    Travel2D _travel = new Travel2D(transform, _path, _Speed.Value);
                    _travel.OnCompleteAction += () => _State = States.Attack;
                }
                else
                {
                    _State = States.Attack;
                }

                _DeltaState = _State;
            }
        }
        void Attack()
        {
            if (_DeltaState != _State)
            {
                _anim.Play("Attack");
                _DeltaState = _State;
            }

            if (_Target.active == false)
            {
                _State = States.BackToOriginalPos;
            }
        }

        public void AttackEvent()
        {
            if(_Target)
            {
                IDamageble _damage = _Target.GetComponent<IDamageble>();
                if (_damage != null)
                {
                    _damage.Damage(_AttackPower.Value);
                }
            }
        }

        void BackToOriginalPos()
        {
            if (_DeltaState != _State)
            {
                _anim.Play("Run");
                List<Vector3> _path = new List<Vector3>();
                _path.Add(transform.position);
                _path.Add(_OriginalPos);
                Travel2D _travel = new Travel2D(transform, _path, _Speed.Value);
                _travel.OnCompleteAction += () => _State = States.Idle;

                _DeltaState = _State;
            }
        }

        public enum States
        {
            None,Idle,FollowEnemy,Attack,BackToOriginalPos
        }
        public States _State;
        public States _DeltaState;
    }
}
