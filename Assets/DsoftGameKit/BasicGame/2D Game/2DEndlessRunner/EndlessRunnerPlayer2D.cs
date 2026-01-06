using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class EndlessRunnerPlayer2D : Player
    {
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] Animator _anim;

        [SerializeField] SimpleStateMachine<EndlessRunnerPlayer2D> _machine = new SimpleStateMachine<EndlessRunnerPlayer2D>();
        SimpleState<EndlessRunnerPlayer2D> _Idle = new Idle();
        SimpleState<EndlessRunnerPlayer2D> _Jump = new Jump();
        SimpleState<EndlessRunnerPlayer2D> _Run = new Run();
        SimpleState<EndlessRunnerPlayer2D> _Fall = new Fall();
        SimpleState<EndlessRunnerPlayer2D> _Dead = new Dead();

        private void Start()
        {
            _machine.CurrentState = _Run;
            GestureControllManager._Instance._OnTap += delegate
            {
                _machine.CurrentState = _Jump;
            };
        }

        public override void OnDie()
        {
            base.OnDie();
            _machine.CurrentState = _Dead;
        }


        private void Update()
        {
            _machine.UpdateState(this);
            GroundCheck();
            GroundParticle();
        }

        private void FixedUpdate()
        {
            _machine.FixedUpdateState(this);
        }

        //---------------------------------------------
        float _MoveSpeed = 10;
        float _JumpSpeed = 20;
        float _HorizontalInput = 1;

        //---------------------------------------------
        [SerializeField] ParticleSystem _groundParticle;
        [SerializeField] ParticleSystem _groundHitParticle;
        bool _deltaGround;
        void GroundParticle()
        {
            if(Ground != _deltaGround)
            {
                _groundHitParticle.Play();
                _deltaGround = Ground;
            }
        }

        //---------------------------------------------
        [SerializeField] bool Ground;
        [SerializeField] float RaycastDistance;
        [SerializeField] LayerMask Mask;
        void GroundCheck()
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance, Mask))
            {
                Ground = true;
            }
            else
            {
                Ground = false;
            }
        }



        class Idle : SimpleState<EndlessRunnerPlayer2D>
        {
            public override void EnterState(EndlessRunnerPlayer2D _machine)
            {
                _machine._anim.CrossFade("Idle", 0f);
                base.EnterState(_machine);
            }
        }

        class Run : SimpleState<EndlessRunnerPlayer2D>
        {
            public override void EnterState(EndlessRunnerPlayer2D _machine)
            {
                _machine._anim.CrossFade("Run", 0f);
                _machine._groundParticle.Play();
                base.EnterState(_machine);
            }

            public override SimpleState<EndlessRunnerPlayer2D> UpdateState(EndlessRunnerPlayer2D _machine)
            {
                if (_machine._rb.velocity.y < 0 && _machine.Ground == false) return _machine._Fall;
                return base.UpdateState(_machine);
            }

            public override void FixedUpdateState(EndlessRunnerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                _machine._rb.velocity = new Vector2(_machine._MoveSpeed * _machine._HorizontalInput, _machine._rb.velocity.y);
            }

            public override void ExitState(EndlessRunnerPlayer2D _machine)
            {
                base.ExitState(_machine);
                _machine._groundParticle.Stop();
            }
        }

        [SerializeField] AudioSource _JumpAudio;
        class Jump : SimpleState<EndlessRunnerPlayer2D>
        {
            bool _jumped;
            public override void EnterState(EndlessRunnerPlayer2D _machine)
            {
                _machine._JumpAudio.Play();
                _machine._anim.CrossFade("Jump", 0f);
                _jumped = false;
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(EndlessRunnerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                if (_jumped == false)
                {
                    Debug.Log("a");
                    _machine._rb.velocity = new Vector2(_machine._rb.velocity.x, _machine._JumpSpeed);
                    _jumped = true;
                }

                _machine._rb.velocity = new Vector2(_machine._MoveSpeed * _machine._HorizontalInput, _machine._rb.velocity.y);
            }

            public override SimpleState<EndlessRunnerPlayer2D> UpdateState(EndlessRunnerPlayer2D _machine)
            {
                if (_machine._rb.velocity.y < 0 && _jumped) return _machine._Fall;
                return base.UpdateState(_machine);
            }
        }

        class Fall : SimpleState<EndlessRunnerPlayer2D>
        {
            public override void EnterState(EndlessRunnerPlayer2D _machine)
            {
                _machine._anim.CrossFade("Fall", 0f);
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(EndlessRunnerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                _machine._rb.velocity = new Vector2(_machine._MoveSpeed * _machine._HorizontalInput, _machine._rb.velocity.y);
            }


            public override SimpleState<EndlessRunnerPlayer2D> UpdateState(EndlessRunnerPlayer2D _machine)
            {
                if(_machine.Ground)
                {
                    if (Mathf.Abs(_machine._HorizontalInput) > 0.5f) return _machine._Run;
                }
                return base.UpdateState(_machine);
            }
        }

        class Dead : SimpleState<EndlessRunnerPlayer2D>
        {
            public override void EnterState(EndlessRunnerPlayer2D _machine)
            {
                _machine._anim.CrossFade("Dead", 0f);
                //WindowManager._Instance._CurrentWindow = WindowManager.WindowTypeEnum.CutsceneWindow;
                Wait _new = new Wait(2);
                _new.OnTimeOutAction += delegate
                {
                    WindowManager._Instance._CurrentWindow = WindowManager.WindowTypeEnum.GameOverWindow;
                    //ApplovinAds.LocalInstance.ShowIfValid();
                };
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(EndlessRunnerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                _machine._rb.velocity = new Vector2(0, _machine._rb.velocity.y);
            }
        }
    }

}
