using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class TopDownRPGSimpleFollowEnemy : Enemy
    {
        SimpleStateMachine<TopDownRPGSimpleFollowEnemy> _machine = new SimpleStateMachine<TopDownRPGSimpleFollowEnemy>();
        SimpleState<TopDownRPGSimpleFollowEnemy> _Idle = new Idle();
        SimpleState<TopDownRPGSimpleFollowEnemy> _Follow = new Follow();
        SimpleState<TopDownRPGSimpleFollowEnemy> _Attack = new Attack();
        SimpleState<TopDownRPGSimpleFollowEnemy> _Hit = new Hit();

        public override void OnEnable()
        {
            base.OnEnable();
            _machine.CurrentState = _Idle;
            _Target.Value = null;

            OnDamageAction += () => _machine.CurrentState = _Hit;
        }

        private void Update()
        {
            _machine.UpdateState(this);
        }

        public GameObject SetTarget
        {
            set
            {
                _Target.Value = value;
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
        [SerializeField] GameObjectReference _Target;
        class Idle : SimpleState<TopDownRPGSimpleFollowEnemy>
        {
            public override void EnterState(TopDownRPGSimpleFollowEnemy _machine)
            {
                _machine._anim.CrossFade("Idle", 0f);
                base.EnterState(_machine);
            }


            public override SimpleState<TopDownRPGSimpleFollowEnemy> UpdateState(TopDownRPGSimpleFollowEnemy _machine)
            {
                if(_machine._Target.Value)
                {
                    return _machine._Follow;
                }
                return base.UpdateState(_machine);
            }
        }

        class Follow : SimpleState<TopDownRPGSimpleFollowEnemy>
        {
            Travel3D _travel;

            public override void EnterState(TopDownRPGSimpleFollowEnemy _machine)
            {
                _machine._anim.CrossFade("Run", 0f);
                /*
                List<Vector3> _path = new List<Vector3>();
                _path.Add(_machine.transform.position);
                _path.Add(_machine._Target.transform.position);
                _travel = new Travel3D(_machine.transform, _path, _machine._Speed.Value);
                _travel.YLock = false;
                _travel.OnCompleteAction += delegate
                {
                    if(_machine._machine.CurrentState == _machine._Follow) _machine._machine.CurrentState = _machine._Attack;
                };
                */
            }

            public override void ExitState(TopDownRPGSimpleFollowEnemy _machine)
            {
                //_travel.Clear();
                base.ExitState(_machine);
            }

            public override SimpleState<TopDownRPGSimpleFollowEnemy> UpdateState(TopDownRPGSimpleFollowEnemy _machine)
            {
                if (_machine._Target.Value)
                {
                    _machine.transform.Translate(new Vector3(0, 0, _machine._Speed.Value * 0.1f), Space.Self);
                    Vector3 _dir = _machine._Target.Value.transform.position - _machine.transform.position;
                    _dir.y = 0;
                    _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), Time.deltaTime * 10);

                    if (Vector3.Distance(_machine.transform.position, _machine._Target.Value.transform.position) < 10)
                    {
                        return _machine._Attack;
                    }
                }
                else
                {
                    return _machine._Idle;
                }
                return base.UpdateState(_machine);
            }
        }

        class Attack : SimpleState<TopDownRPGSimpleFollowEnemy>
        {
            public override void EnterState(TopDownRPGSimpleFollowEnemy _machine)
            {
                _machine._anim.CrossFade("Attack", 0f);
                base.EnterState(_machine);
            }

            public override SimpleState<TopDownRPGSimpleFollowEnemy> UpdateState(TopDownRPGSimpleFollowEnemy _machine)
            {
                if (_machine._Target.Value)
                {
                    if(_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime - (int)_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                    {
                        if (Vector3.Distance(_machine.transform.position, _machine._Target.Value.transform.position) > 10)
                        {
                            return _machine._Follow;
                        }
                    }
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


        class Hit : SimpleState<TopDownRPGSimpleFollowEnemy>
        {
            public override void EnterState(TopDownRPGSimpleFollowEnemy _machine)
            {
                _machine._anim.CrossFade("Hit", 0f);
                base.EnterState(_machine);
            }

            public override SimpleState<TopDownRPGSimpleFollowEnemy> UpdateState(TopDownRPGSimpleFollowEnemy _machine)
            {
                if(_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    return _machine._Idle;
                }

                return base.UpdateState(_machine);
            }

        }
    }

}
