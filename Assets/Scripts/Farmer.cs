using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Farmer : MonoBehaviour
    {
        [SerializeField] SimpleStateMachine<Farmer> _machine = new SimpleStateMachine<Farmer>();
        SimpleState<Farmer> _Idle = new Idle();
        SimpleState<Farmer> _Run = new Run();
        SimpleState<Farmer> _Attack = new Attack();


        [SerializeField] float _HorizotalInput;
        [SerializeField] float _VerticalInput;
        [SerializeField] float _MoveSpeed;
        [SerializeField] float _AttackInput;

        Enemy _Target;
        [SerializeField] Animator _anim;
        Rigidbody _rb;

        private void Start()
        {
            //_anim = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }

        public void OnEnable()
        {
            _machine.CurrentState = _Idle;
        }

        Camera _cam;
        void InputUpdate()
        {
            if (_cam == null) _cam = Camera.main;
            _HorizotalInput = EasyInputManager._Instance._Data._Joysticks[0].x;
            _VerticalInput = EasyInputManager._Instance._Data._Joysticks[0].y;

            if (Mathf.Abs(_HorizotalInput) > 0 | Mathf.Abs(_VerticalInput) > 0) GameManager._Instance._OnPlayerInteract?.Invoke();
        }

        private void Update()
        {
            _machine.UpdateState(this);
            InputUpdate();
        }

        private void FixedUpdate()
        {
            _machine.FixedUpdateState(this);
        }

        class Idle : SimpleState<Farmer>
        {
            public override void EnterState(Farmer _machine)
            {
                _machine._anim.CrossFade("Idle" , 0.1f);
                base.EnterState(_machine);
            }

            void findattackobject(Farmer _machine)
            {
                Enemy[] _all = FindObjectsOfType<Enemy>();

                foreach (var item in _all)
                {
                    if (Vector3.Distance(item.transform.position, _machine.transform.position) < 2 && item.Health > 0)
                    {
                        _machine._Target = item;
                    }
                }
            }

            public override SimpleState<Farmer> UpdateState(Farmer _machine)
            {
                findattackobject(_machine);
                if (_machine._Target) return _machine._Attack;
                if (_machine._HorizotalInput != 0 | _machine._VerticalInput != 0) return _machine._Run;
                return base.UpdateState(_machine);
            }
        }

        class Run : SimpleState<Farmer>
        {
            public override void EnterState(Farmer _machine)
            {
                _machine._anim.CrossFade("Run" , 0.1f);
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(Farmer _machine)
            {
                //Vector2 _input = new Vector2(_machine._HorizotalInput, _machine._VerticalInput).normalized * _machine._MoveSpeed;
                Vector3 _forword = Vector3.Cross(_machine._cam.transform.right, Vector3.up);
                Vector3 _dir = (_machine._cam.transform.right * _machine._HorizotalInput + _forword * _machine._VerticalInput).normalized;
                _machine._rb.velocity = _dir * _machine._MoveSpeed;

                if (_dir != Vector3.zero)
                {
                    _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up) , Time.deltaTime * 10);
                }
                
            }

            public override SimpleState<Farmer> UpdateState(Farmer _machine)
            {
                if (_machine._HorizotalInput == 0 && _machine._VerticalInput == 0) return _machine._Idle;
                if (_machine._AttackInput == 1) return _machine._Attack;
                return base.UpdateState(_machine);
            }

            public override void ExitState(Farmer _machine)
            {
                _machine._rb.velocity = new Vector3(0, _machine._rb.velocity.y, 0);
                base.ExitState(_machine);
            }
        }

        class Attack : SimpleState<Farmer>
        {
            public override void EnterState(Farmer _machine)
            {
                _machine._anim.CrossFade("Attack", 0.1f);
                Wait _new = new Wait(1);
                _new.OnTimeOutAction += delegate
                {
                    if(_machine._Target) _machine._Target.Damage(1);
                    _machine._Target = null;
                    _machine._machine.CurrentState = _machine._Idle;
                };
                base.EnterState(_machine);
            }

            public override SimpleState<Farmer> UpdateState(Farmer _machine)
            {
                if (_machine._HorizotalInput != 0 | _machine._VerticalInput != 0) return _machine._Run;
                return base.UpdateState(_machine);
            }

            public override void ExitState(Farmer _machine)
            {
                _machine._Target = null;
                base.ExitState(_machine);
            }
        }

    }
}
