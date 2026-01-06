using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class TopDownRPGPlayer : Player
    {
        [SerializeField] SimpleStateMachine<TopDownRPGPlayer> _machine = new SimpleStateMachine<TopDownRPGPlayer>();
        SimpleState<TopDownRPGPlayer> _Idle = new Idle();
        SimpleState<TopDownRPGPlayer> _Run = new Run();
        SimpleState<TopDownRPGPlayer> _Attack = new Attack();
        SimpleState<TopDownRPGPlayer> _Dash = new Dash();


        [SerializeField] FloatReference _HorizotalInput;
        [SerializeField] FloatReference _VerticalInput;
        [SerializeField] FloatReference _MoveSpeed;
        [SerializeField] FloatReference _AttackInput;
        [SerializeField] FloatReference _DashInput;
        [SerializeField] FloatReference _Stamina;
        Animator _anim;
        Rigidbody _rb;
        [SerializeField] GameObjectReference _Target;
        BaseCharacter _TargetBase;
        public GameObject SetTarget
        {
            set
            {
                _Target.Value = value;
                _TargetBase = value.GetComponent<BaseCharacter>();
            }
        }

        public GameObject SetTargetNull
        {
            set
            {
               //if(value == _Target) _Target = null;
            }
        }

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _machine.CurrentState = _Idle;
        }

        private void Update()
        {
            _machine.UpdateState(this);
            GroundStick();
            NullTarget();
        }

        void NullTarget()
        {
            if (_Target.Value && Vector3.Distance(_Target.Value.transform.position, transform.position) > 20) _Target.Value = null;
            if (_Target.Value && _TargetBase.IsAlive == false) _Target.Value = null;
        }

        private void FixedUpdate()
        {
            _machine.FixedUpdateState(this);
        }


        void GroundStick()
        {
            Ray _ray = new Ray(transform.position, Vector3.down);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit))
            {
                transform.position = new Vector3(transform.position.x, _hit.point.y, transform.position.z);
            }
        }

        class Idle : SimpleState<TopDownRPGPlayer>
        {
            public override void EnterState(TopDownRPGPlayer _machine)
            {
                _machine._anim.CrossFade("Idle" , 0.1f);
                base.EnterState(_machine);
            }

            public override SimpleState<TopDownRPGPlayer> UpdateState(TopDownRPGPlayer _machine)
            {
                if (_machine._HorizotalInput.Value != 0 | _machine._VerticalInput.Value != 0) return _machine._Run;
                if (_machine._AttackInput.Value == 1) return _machine._Attack;
                if (_machine._DashInput.Value == 1 && _machine._Stamina.Value > 25) return _machine._Dash;

                return base.UpdateState(_machine);
            }
        }

        class Run : SimpleState<TopDownRPGPlayer>
        {
            public override void EnterState(TopDownRPGPlayer _machine)
            {
                _machine._anim.CrossFade("Run" , 0.1f);
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(TopDownRPGPlayer _machine)
            {
                Vector2 _input = new Vector2(_machine._HorizotalInput.Value, _machine._VerticalInput.Value).normalized * _machine._MoveSpeed.Value;
                _machine._rb.velocity = new Vector3(_input.x, _machine._rb.velocity.y, _input.y);

                Vector3 _dir = new Vector3(_machine._HorizotalInput.Value, 0, _machine._VerticalInput.Value);
                _dir.y = 0;
                if (_dir != Vector3.zero)
                {
                    _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up) , Time.deltaTime * 10);
                }
            }

            public override SimpleState<TopDownRPGPlayer> UpdateState(TopDownRPGPlayer _machine)
            {
                if (_machine._HorizotalInput.Value == 0 && _machine._VerticalInput.Value == 0) return _machine._Idle;
                if (_machine._AttackInput.Value == 1) return _machine._Attack;
                if (_machine._DashInput.Value == 1 && _machine._Stamina.Value > 25) return _machine._Dash;

                return base.UpdateState(_machine);
            }

            public override void ExitState(TopDownRPGPlayer _machine)
            {
                _machine._rb.velocity = new Vector3(0, _machine._rb.velocity.y, 0);
                base.ExitState(_machine);
            }
        }

        class Attack : SimpleState<TopDownRPGPlayer>
        {
            public override void EnterState(TopDownRPGPlayer _machine)
            {
                _machine._anim.CrossFade("Attack", 0.1f);
                _count = -1;
                base.EnterState(_machine);
            }


            int _count;
            public override SimpleState<TopDownRPGPlayer> UpdateState(TopDownRPGPlayer _machine)
            {
                if (_machine._anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (int)_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime != _count)
                {
                    _count = (int)_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                }

                if (_machine._anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && _machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime - (int) _machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
                {
                    if(_machine._AttackInput.Value == 0)
                    {
                        if (_machine._HorizotalInput.Value == 0 && _machine._VerticalInput.Value == 0)
                        {
                            return _machine._Idle;
                        }
                        else
                        {
                            return _machine._Run;
                        }
                    }
                }

                if (_machine._DashInput.Value == 1 && _machine._Stamina.Value > 25) return _machine._Dash;

                if (_machine._Target.Value && _machine._Target.Value.active)
                {
                    _machine.transform.position = Vector3.Lerp(_machine.transform.position, _machine._Target.Value.transform.position + ((_machine.transform.position - _machine._Target.Value.transform.position).normalized * 10), Time.deltaTime * 10);
                    Vector3 _dir = _machine._Target.Value.transform.position - _machine.transform.position;
                    _dir.y = 0;
                    if(_dir != Vector3.zero) _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), Time.deltaTime * 10);
                }
                else
                {
                    if (_machine._HorizotalInput.Value != 0 && _machine._VerticalInput.Value != 0)
                    {
                        Vector3 _dir = new Vector3(_machine._HorizotalInput.Value, 0, _machine._VerticalInput.Value);
                        _dir.y = 0;
                        if(_dir != Vector3.zero) _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), Time.deltaTime * 10);
                    }
                }

                return base.UpdateState(_machine);
            }
        }

        class Dash : SimpleState<TopDownRPGPlayer>
        {
            public override void EnterState(TopDownRPGPlayer _machine)
            {
                _machine._anim.CrossFade("Dash", 0.1f);
                _machine._Stamina.Value -= 25;
                base.EnterState(_machine);
            }

            public override SimpleState<TopDownRPGPlayer> UpdateState(TopDownRPGPlayer _machine)
            {
                if (_machine._AttackInput.Value == 1)
                {
                    return _machine._Attack;
                }
                else if(_machine._anim.GetCurrentAnimatorStateInfo(0).IsName("Dash") && _machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    if (_machine._HorizotalInput.Value == 0 && _machine._VerticalInput.Value == 0)
                    {
                        return _machine._Idle;
                    }
                    else
                    {
                        return _machine._Run;
                    }
                }
                return base.UpdateState(_machine);
            }


            public override void FixedUpdateState(TopDownRPGPlayer _machine)
            {
                Vector3 _dir = _machine.transform.forward * 200;
                _machine._rb.velocity = new Vector3(_dir.x, _machine._rb.velocity.y, _dir.z);
                base.FixedUpdateState(_machine);
            }

            public override void ExitState(TopDownRPGPlayer _machine)
            {
                _machine._DashInput.Value = 0;
                _machine._rb.velocity = new Vector3(0, _machine._rb.velocity.y, 0);
                base.ExitState(_machine);
            }
        }

    }
}
