using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace DsoftGameKit
{
    public class TowerDefensePlayer2DTowerSoldire : Player, ITowerDefenseTarget2D
    {
        public GameObject GetTarget()
        {
            return _Target;
        }
        public void SetTarget(GameObject _value)
        {

        }

        Animator _anim;
        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _machine.CurrentState = _WalkToPos;
            _Target = null;
        }


        [SerializeField] GameObjectReference _PathObject;
        Path2D _Path;
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
                if(_Target == null)
                {

                    if (value.GetComponent<ITowerDefenseTarget2D>().GetTarget() == null)
                    {
                        _Target = value;
                        value.GetComponent<ITowerDefenseTarget2D>().SetTarget(gameObject);
                    }
                    else
                    {
                        if (value.GetComponent<ITowerDefenseTarget2D>().GetTarget().active == false)
                        {
                            _Target = value;
                            value.GetComponent<ITowerDefenseTarget2D>().SetTarget(gameObject);
                        }
                    }
                }
            }
        }


        SimpleStateMachine<TowerDefensePlayer2DTowerSoldire> _machine = new SimpleStateMachine<TowerDefensePlayer2DTowerSoldire>();
        SimpleState<TowerDefensePlayer2DTowerSoldire> _WalkToPos = new WalkToPos();
        SimpleState<TowerDefensePlayer2DTowerSoldire> _Idle = new Idle();
        SimpleState<TowerDefensePlayer2DTowerSoldire> _Attack = new Attack();
        SimpleState<TowerDefensePlayer2DTowerSoldire> _walkToEnemy = new WalkToEnemy();

        public string _state;
        private void Update()
        {
            _machine.UpdateState(this);

            if (_Target && _Target.active == false)
            {
                _Target = null;
            }
        }

        public void AttackEvent()
        {
            if (_Target)
            {
                IDamageble _damage = _Target.GetComponent<IDamageble>();
                if (_damage != null)
                {
                    _damage.Damage(_AttackPower.Value);
                }
            }
        }


        class WalkToPos : SimpleState<TowerDefensePlayer2DTowerSoldire>
        {
            public override void EnterState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                _init = false;
                base.EnterState(_machine);
            }
            bool _init = false;

            Travel2D _travel;
            public override SimpleState<TowerDefensePlayer2DTowerSoldire> UpdateState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                if(_machine._PathObject.Value && _init == false)
                {
                    _machine._state = "WalkToPos";
                    _machine._Path = _machine._PathObject.Value.GetComponent<Path2D>();
                    _machine._anim.Play("Run");
                    Vector3 _pos = _machine._Path.GetClosesWaypoint(_machine.transform.position).transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                    List<Vector3> _road = new List<Vector3>();
                    _road.Add(_machine.transform.position);
                    _road.Add(_pos);
                    _travel = new Travel2D(_machine.transform, _road, _machine._Speed.Value);
                    _travel.OnCompleteAction += () => _machine._machine.CurrentState = _machine._Idle;
                    _init = true;
                }

                if(_machine._Target)
                {
                    _travel.Clear();
                    return _machine._walkToEnemy;
                }

                return base.UpdateState(_machine);
            }
        }
        class Idle : SimpleState<TowerDefensePlayer2DTowerSoldire>
        {
            public override void EnterState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                _machine._state = "Id;e";
                _machine._anim.Play("Idle");
                base.EnterState(_machine);
            }

            public override SimpleState<TowerDefensePlayer2DTowerSoldire> UpdateState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                if(_machine._Target)
                {
                    return _machine._walkToEnemy;
                }
                return base.UpdateState(_machine);
            }
        }
        class Attack : SimpleState<TowerDefensePlayer2DTowerSoldire>
        {
            public override void EnterState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                _machine._state = "Attack";
                _machine._anim.Play("Attack");
                base.EnterState(_machine);
            }

            public override SimpleState<TowerDefensePlayer2DTowerSoldire> UpdateState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                if(_machine.Target == null)
                {
                    return _machine._WalkToPos;
                }

                Vector3 _dir = _machine._Target.transform.position - _machine.transform.position;
                if (_dir.x > 0)
                {
                    _machine.transform.localScale = new Vector3(1, 1, 1);
                }
                if (_dir.x < 0)
                {
                    _machine.transform.localScale = new Vector3(-1, 1, 1);
                }

                return base.UpdateState(_machine);
            }
        }
        class WalkToEnemy : SimpleState<TowerDefensePlayer2DTowerSoldire>
        {
            Travel2D _travel;
            public override void EnterState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                _machine._state = "WalkToEnemy";

                _machine._anim.Play("Run");

                Vector3 _pos = _machine._Target.transform.position + _machine._Target.transform.right * 0.5f;
                List<Vector3> _road = new List<Vector3>();
                _road.Add(_machine.transform.position);
                _road.Add(_pos);
                _travel = new Travel2D(_machine.transform, _road, _machine._Speed.Value);
                _travel.OnCompleteAction += delegate
                {
                    _machine._machine.CurrentState = _machine._Attack;
                };

                base.EnterState(_machine);
            }

            public override SimpleState<TowerDefensePlayer2DTowerSoldire> UpdateState(TowerDefensePlayer2DTowerSoldire _machine)
            {
                if(_machine._Target == null)
                {
                    _travel.Clear();
                    return _machine._WalkToPos;
                }
                return base.UpdateState(_machine);
            }
        }

    }



}
