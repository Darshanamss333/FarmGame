using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DsoftGameKit
{
    public class ARPGPlayer : Player
    {
        
        [SerializeField] SimpleStateMachine<ARPGPlayer> _machine = new SimpleStateMachine<ARPGPlayer>();
        SimpleState<ARPGPlayer> _Idle = new Idle();
        SimpleState<ARPGPlayer> _Run = new Run();
        SimpleState<ARPGPlayer> _Attack = new Attack();
        SimpleState<ARPGPlayer> _Dash = new Dash();


        [SerializeField] FloatReference _MoveSpeed;
        [SerializeField] FloatReference _Stamina;
        Animator _anim;

        [SerializeField] GameObjectReference _Target;
        [SerializeField] Vector3Reference _TargetPos;
        Vector3 _deltaTargetPos;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }
        public override void OnEnable()
        {
            base.OnEnable();
            _TargetPos.Value = Vector3.zero;
            _deltaTargetPos = _TargetPos.Value;
            _Target.Value = null;
            _machine.CurrentState = _Idle;
        }

        private void Update()
        {
            _machine.UpdateState(this);
            GroundStick();

            if (_Target.Value && !_Target.Value.activeInHierarchy) _Target.Value = null;
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

        class Idle : SimpleState<ARPGPlayer>
        {
            public override void EnterState(ARPGPlayer _machine)
            {
                _machine._anim.CrossFade("Idle" , 0.1f);
                base.EnterState(_machine);
            }

            public override SimpleState<ARPGPlayer> UpdateState(ARPGPlayer _machine)
            {
                if(_machine._deltaTargetPos != _machine._TargetPos.Value)
                {
                    return _machine._Run;
                }

                return base.UpdateState(_machine);
            }
        }

        class Run : SimpleState<ARPGPlayer>
        {
            Travel3D _travel;
            public override void EnterState(ARPGPlayer _machine)
            {
                _machine._anim.CrossFade("Run" , 0.1f);
                base.EnterState(_machine);

                _travel = new Travel3D(_machine.transform, new List<Vector3>(), 0);
            }


            public override SimpleState<ARPGPlayer> UpdateState(ARPGPlayer _machine)
            {
                Vector3 _dir = (_machine._TargetPos.Value - _machine.transform.position).normalized;
                _dir.y = 0;
                if (_dir != Vector3.zero)
                {
                    _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), Time.deltaTime * 10);
                }

                if(_machine._TargetPos.Value != _machine._deltaTargetPos)
                {
                    _travel.Clear();

                    NavMeshPath _nevPath = new NavMeshPath();
                    NavMesh.CalculatePath(_machine.transform.position, _machine._TargetPos.Value, NavMesh.AllAreas, _nevPath);
                    List<Vector3> _path = new List<Vector3>();

                    for (int i = 0; i < _nevPath.corners.Length; i++)
                    {
                        _path.Add(_nevPath.corners[i]);
                    }

                    _travel = new Travel3D(_machine.transform, _path, _machine._MoveSpeed.Value);
                    _travel.YLookAt = false;
                    _travel.OnCompleteAction += delegate
                    {
                        if (_machine._machine.CurrentState == _machine._Run && _machine._Target.Value && _machine._Target.Value.activeInHierarchy) _machine._machine.CurrentState = _machine._Attack;
                        if (_machine._machine.CurrentState == _machine._Run && _machine._Target.Value == null) _machine._machine.CurrentState = _machine._Idle;
                    };

                    _machine._deltaTargetPos = _machine._TargetPos.Value;
                }

                return base.UpdateState(_machine);
            }

            public override void ExitState(ARPGPlayer _machine)
            {
                _travel.Clear();
                base.ExitState(_machine);
            }
        }

        class Attack : SimpleState<ARPGPlayer>
        {
            
            public override void EnterState(ARPGPlayer _machine)
            {
                _machine._anim.CrossFade("Attack", 0.1f);
                _count = -1;
                base.EnterState(_machine);
            }


            int _count;
            public override SimpleState<ARPGPlayer> UpdateState(ARPGPlayer _machine)
            {
                if (_machine._anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && (int)_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime != _count)
                {
                    _count = (int)_machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                }

                if (_machine._anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && _machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime - (int) _machine._anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
                {
                    if (_machine._Target.Value == null)
                    {
                        return _machine._Idle;
                    }
                }

                if (_machine._deltaTargetPos != _machine._TargetPos.Value)
                {
                    return _machine._Run;
                }

                if (_machine._Target.Value)
                {
                    Vector3 _dir = _machine._Target.Value.transform.position - _machine.transform.position;
                    _dir.y = 0;
                    if(_dir != Vector3.zero) _machine.transform.rotation = Quaternion.Lerp(_machine.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), Time.deltaTime * 10);
                }

                return base.UpdateState(_machine);
            }
            
        }

        class Dash : SimpleState<ARPGPlayer>
        {

        }

    }
}
