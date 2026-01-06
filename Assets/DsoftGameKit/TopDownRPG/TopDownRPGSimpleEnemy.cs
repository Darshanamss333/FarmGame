using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class TopDownRPGSimpleEnemy : Enemy
    {
        SimpleStateMachine<TopDownRPGSimpleEnemy> _machine = new SimpleStateMachine<TopDownRPGSimpleEnemy>();
        SimpleState<TopDownRPGSimpleEnemy> _Idle = new Idle();
        SimpleState<TopDownRPGSimpleEnemy> _Patrol = new Patrol();
        SimpleState<TopDownRPGSimpleEnemy> _BackToOriginalPos = new BactoOriginalPos();
        SimpleState<TopDownRPGSimpleEnemy> _Attack = new Attack();
        SimpleState<TopDownRPGSimpleEnemy> _Hit = new Hit();

        public override void OnEnable()
        {
            base.OnEnable();
            _machine.CurrentState = _Idle;
            _startPos = transform.position;
        }

        private void Update()
        {
            _machine.UpdateState(this);
        }

        public GameObject SetTarget
        {
            set
            {
                _Target = value;
            }
        }


        public GameObject SetTargetNull
        {
            set
            {
                _Target = null;
            }
        }


        [SerializeField] Animator _anim;
        [SerializeField] FloatReference _Speed;
        GameObject _Target;
        Vector3 _startPos;
        class Idle : SimpleState<TopDownRPGSimpleEnemy>
        {
            public override void EnterState(TopDownRPGSimpleEnemy _machine)
            {
                _machine._anim.CrossFade("Idle", 0.1f);
                Wait _new = new Wait(Random.Range(5, 10));
                _new.OnTimeOutAction += delegate
                {
                    if(_machine._machine.CurrentState == _machine._Idle) _machine._machine.CurrentState = _machine._Patrol;
                };
                base.EnterState(_machine);
            }


            public override SimpleState<TopDownRPGSimpleEnemy> UpdateState(TopDownRPGSimpleEnemy _machine)
            {
                if(_machine._Target)
                {
                    return _machine._Attack;
                }
                return base.UpdateState(_machine);
            }
        }

        class Patrol : SimpleState<TopDownRPGSimpleEnemy>
        {
            Travel3D _travel;

            public override void EnterState(TopDownRPGSimpleEnemy _machine)
            {
                _machine._anim.CrossFade("Run", 0.1f);
                Vector3 _patPos = _machine.transform.position + new Vector3(Random.Range(-20, 20),0, Random.Range(-20, 20));
                List<Vector3> _path = new List<Vector3>();
                _path.Add(_machine.transform.position);
                _path.Add(_patPos);
                _travel = new Travel3D(_machine.transform, _path, _machine._Speed.Value);
                _travel.YLock = false;
                _travel.OnCompleteAction += delegate
                {
                    _machine._machine.CurrentState = _machine._BackToOriginalPos;
                };
            }

            public override void ExitState(TopDownRPGSimpleEnemy _machine)
            {
                _travel.Clear();
                base.ExitState(_machine);
            }

            public override SimpleState<TopDownRPGSimpleEnemy> UpdateState(TopDownRPGSimpleEnemy _machine)
            {
                if (_machine._Target)
                {
                    return _machine._Attack;
                }
                return base.UpdateState(_machine);
            }
        }

        class BactoOriginalPos : SimpleState<TopDownRPGSimpleEnemy>
        {
            Travel3D _travel;
            public override void EnterState(TopDownRPGSimpleEnemy _machine)
            {
                _machine._anim.CrossFade("Run", 0.1f);
                List<Vector3> _path = new List<Vector3>();
                _path.Add(_machine.transform.position);
                _path.Add(_machine._startPos);
                _travel = new Travel3D(_machine.transform, _path, _machine._Speed.Value);
                _travel.YLock = false;
                _travel.OnCompleteAction += delegate
                {
                    _machine._machine.CurrentState = _machine._Idle;
                };
            }

            public override void ExitState(TopDownRPGSimpleEnemy _machine)
            {
                _travel.Clear();
                base.ExitState(_machine);
            }

            public override SimpleState<TopDownRPGSimpleEnemy> UpdateState(TopDownRPGSimpleEnemy _machine)
            {
                if (_machine._Target)
                {
                    return _machine._Attack;
                }
                return base.UpdateState(_machine);
            }
        }

        class Attack : SimpleState<TopDownRPGSimpleEnemy>
        {
            public override void EnterState(TopDownRPGSimpleEnemy _machine)
            {
                _machine._anim.CrossFade("Attack", 0.1f);
                base.EnterState(_machine);
            }

            public override SimpleState<TopDownRPGSimpleEnemy> UpdateState(TopDownRPGSimpleEnemy _machine)
            {
                if (_machine._Target)
                {
                    Vector3 _dir = _machine._Target.transform.position - _machine.transform.position;
                    _dir.y = 0;
                    _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), Time.deltaTime * 10);
                }
                else
                {
                    if((_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime - (int)_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime) < 0.1f)
                    {
                        return _machine._Idle;
                    }
                }

                return base.UpdateState(_machine);
            }
        }


        class Hit : SimpleState<TopDownRPGSimpleEnemy>
        {
            public override void EnterState(TopDownRPGSimpleEnemy _machine)
            {
                _machine._anim.CrossFade("Hit", 0f);
                base.EnterState(_machine);
            }

            public override SimpleState<TopDownRPGSimpleEnemy> UpdateState(TopDownRPGSimpleEnemy _machine)
            {
                if (_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                {
                    return _machine._Attack;
                }
                return base.UpdateState(_machine);
            }
        }
    }

}
