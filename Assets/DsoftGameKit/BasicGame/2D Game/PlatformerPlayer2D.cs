using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public interface IPlayerMarioEvents
    {
        public void Refresh(bool _value);
    }

    public class PlatformerPlayer2D : Player
    {
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] Animator _anim;

        [SerializeField] SimpleStateMachine<PlatformerPlayer2D> _machine = new SimpleStateMachine<PlatformerPlayer2D>();
        SimpleState<PlatformerPlayer2D> _Idle = new Idle();
        SimpleState<PlatformerPlayer2D> _Jump = new Jump();
        SimpleState<PlatformerPlayer2D> _Run = new Run();
        SimpleState<PlatformerPlayer2D> _Fall = new Fall();
        SimpleState<PlatformerPlayer2D> _Dead = new Dead();
        SimpleState<PlatformerPlayer2D> _Hit = new Hit();
        //SimpleState<PlatformerPlayer2D> _Slide = new Slide();

        private void Start()
        {
            _machine.CurrentState = _Idle;
            GameManager._Instance._player = this;
        }

        public override void OnDie()
        {
            base.OnDie();
            _machine.CurrentState = _Dead;
        }

        public override void Damage(float _value)
        {
            base.Damage(_value);
        }

        public override void OnHit()
        {
            base.OnHit();
            _machine.CurrentState = _Hit;
        }

        public override void Update()
        {
            base.Update();
            _machine.UpdateState(this);
            InputUpdate();
            Flip();
            GroundCheck();
            GroundParticle();
            SlideCheck();
            Acc();
        }

        private void FixedUpdate()
        {
            _machine.FixedUpdateState(this);
        }

        //---------------------------------------------
        float _MoveSpeed = 8;
        float _JumpSpeed = 20;
        float _MaxJumpHight = 4;
        float _HorizontalInput;
        float _JumpInput;
        void InputUpdate()
        {
            _HorizontalInput = Mathf.Lerp(_HorizontalInput, EasyInputManager._Instance._Data._Joysticks[0].x, Time.deltaTime * 5);
            _JumpInput = EasyInputManager._Instance._Data._Buttons[0];
        }


        //---------------------------------------------
        float _moveacc;
        void Acc()
        {
            _moveacc = Mathf.Lerp(_moveacc, _HorizontalInput * _MoveSpeed, Time.deltaTime * 10);
        }

        //---------------------------------------------
        void Flip()
        {
            float _dir = _rb.velocity.x;
            if(_dir != 0)
            {
                if (_dir > 0.3f)
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }

                if (_dir < -0.3f)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
            }
        }


        //---------------------------------------------
        bool _SlideAllow;
        void SlideCheck()
        {
            if(Ground)
            {
                Ray2D ray = new Ray2D(transform.position + new Vector3(0,3,0), Vector2.down);
                RaycastHit2D _hitone = Physics2D.Raycast(ray.origin + new Vector2(-3, 0), ray.direction, 8, Mask);
                RaycastHit2D _hittwo = Physics2D.Raycast(ray.origin + new Vector2(3,0), ray.direction, 8, Mask);
                float _angle = Vector2.Angle(Vector2.right, _hittwo.point - _hitone.point);

                if(_SlideAllow == false && _angle > 15)
                {
                    if (transform.localScale.x == 1 && _hittwo.point.y < _hitone.point.y)
                    {
                        _SlideAllow = true;
                    }
                    else if(transform.localScale.x == -1 && _hittwo.point.y > _hitone.point.y)
                    {
                        _SlideAllow = true;
                    }
                }

                if(_SlideAllow == true && _angle < 5)
                {
                    _SlideAllow = false;
                }

            }
            else
            {
                if (_SlideAllow == true)
                {
                    _SlideAllow = false;
                }
            }
        }


        //---------------------------------------------
        [SerializeField] ParticleSystem _groundParticle;
        [SerializeField] ParticleSystem _jumpParticle;
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
        IPlayerMarioEvents _iplayermarioevents;
        void GroundCheck()
        {
            RaycastHit2D _hit = Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance, Mask);
            if (_hit.collider)
            {
                if (_iplayermarioevents == null && Ground == false) _iplayermarioevents = _hit.transform.GetComponent<IPlayerMarioEvents>();
                Ground = true;
                transform.parent = _hit.collider.transform;
                if (_iplayermarioevents != null)
                {
                    _iplayermarioevents.Refresh(true);
                }
            }
            else
            {
                Ground = false;
                transform.parent = null;
                if (_iplayermarioevents != null)
                {
                    _iplayermarioevents.Refresh(false);
                    _iplayermarioevents = null;
                }

                transform.rotation = Quaternion.identity;
            }
        }



        class Idle : SimpleState<PlatformerPlayer2D>
        {
            public override void EnterState(PlatformerPlayer2D _machine)
            {
                _machine._anim.CrossFade("Idle", 0f);
                base.EnterState(_machine);
            }

            public override SimpleState<PlatformerPlayer2D> UpdateState(PlatformerPlayer2D _machine)
            {
                //if (_machine._SlideAllow) return _machine._Slide;
                if (Mathf.Abs(_machine._HorizontalInput) > 0.5f) return _machine._Run;
                if (_machine._JumpInput == 1) return _machine._Jump;
                //if (_machine.Ground && _machine._SlideAngle > 25) return _machine._Slide;
                //if (_machine._rb.velocity.y < 0) return _machine._Fall;

                return base.UpdateState(_machine);
            }

            public override void FixedUpdateState(PlatformerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                _machine._rb.velocity = new Vector2(_machine._moveacc, _machine._rb.velocity.y);
            }
        }

        class Run : SimpleState<PlatformerPlayer2D>
        {
            public override void EnterState(PlatformerPlayer2D _machine)
            {
                _machine._anim.CrossFade("Run", 0f);
                //_machine._groundParticle.Play();
                base.EnterState(_machine);
            }

            public override SimpleState<PlatformerPlayer2D> UpdateState(PlatformerPlayer2D _machine)
            {
                //if (_machine._SlideAllow) return _machine._Slide;
                if (Mathf.Abs(_machine._HorizontalInput) < 0.5f) return _machine._Idle;
                if (_machine._JumpInput == 1) return _machine._Jump;
                if (_machine._rb.velocity.y < 0 && _machine.Ground == false) return _machine._Fall;
                return base.UpdateState(_machine);
            }
            public override void FixedUpdateState(PlatformerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                //_machine._rb.velocity = new Vector2(_machine._MoveSpeed * _machine._HorizontalInput, _machine._rb.velocity.y);
                _machine._rb.velocity = new Vector2(_machine._moveacc, _machine._rb.velocity.y);
            }

            public override void ExitState(PlatformerPlayer2D _machine)
            {
                base.ExitState(_machine);
                //_machine._groundParticle.Stop();
            }
        }

        [SerializeField] AudioSource _JumpAudio;
        class Jump : SimpleState<PlatformerPlayer2D>
        {
            bool _jumped;
            public override void EnterState(PlatformerPlayer2D _machine)
            {
                _machine._JumpAudio.Play();
                _machine._anim.CrossFade("Jump", 0f);
                _jumped = false;
                _machine._jumpParticle.Play();
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(PlatformerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                if (_jumped == false)
                {
                    _machine._rb.velocity = new Vector2(_machine._rb.velocity.x, _machine._JumpSpeed);
                    _jumped = true;
                }

                _machine._rb.velocity = new Vector2(_machine._moveacc, _machine._rb.velocity.y);
            }

            public override SimpleState<PlatformerPlayer2D> UpdateState(PlatformerPlayer2D _machine)
            {
                if (_machine._rb.velocity.y < 0) return _machine._Fall;
                return base.UpdateState(_machine);
            }

            public override void ExitState(PlatformerPlayer2D _machine)
            {
                _machine._jumpParticle.Stop();
                base.ExitState(_machine);
            }
        }

        class Fall : SimpleState<PlatformerPlayer2D>
        {
            public override void EnterState(PlatformerPlayer2D _machine)
            {
                _machine._jumpParticle.Play();
                _machine._anim.CrossFade("Fall", 0f);
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(PlatformerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                _machine._rb.velocity = new Vector2(_machine._moveacc, _machine._rb.velocity.y);
            }


            public override SimpleState<PlatformerPlayer2D> UpdateState(PlatformerPlayer2D _machine)
            {
                if(_machine.Ground)
                {
                    if (_machine._JumpInput == 1) return _machine._Jump;
                    if (Mathf.Abs(_machine._HorizontalInput) > 0.5f) return _machine._Run;
                    if (Mathf.Abs(_machine._HorizontalInput) < 0.5f) return _machine._Idle;
                }
                return base.UpdateState(_machine);
            }

            public override void ExitState(PlatformerPlayer2D _machine)
            {
                _machine._jumpParticle.Stop();
                base.ExitState(_machine);
            }
        }

        class Dead : SimpleState<PlatformerPlayer2D>
        {
            public override void EnterState(PlatformerPlayer2D _machine)
            {
                _machine._rb.isKinematic = true;
                _machine._rb.simulated = false;
                _machine._anim.CrossFade("Dead", 0f);
                //WindowManager._Instance._CurrentWindow = WindowManager.WindowTypeEnum.CutsceneWindow;
                Wait _droptime = new Wait(2);
                _droptime.OnWaitAction += delegate
                {
                    if(_droptime.Tang < 0.5f)
                    {
                        float _t = Mathf.InverseLerp(0, 0.5f, _droptime.Tang);
                        _machine.transform.position = Vector3.Lerp(_machine.transform.position, _machine.transform.position + new Vector3(0, Mathf.Lerp(0.5f, 0, _t), 0), _t);
                    }
                    else
                    {
                        float _t = Mathf.InverseLerp(0.5f, 1f, _droptime.Tang);
                        _machine.transform.position = Vector3.Lerp(_machine.transform.position, _machine.transform.position + new Vector3(0, Mathf.Lerp(0, -0.5f, _t), 0), _droptime.Tang);
                    }
                };

                _droptime.OnTimeOutAction += delegate
                {
                    WindowManager._Instance._CurrentWindow = WindowManager.WindowTypeEnum.GameOverWindow;
                    //ApplovinAds._Instance.ShowIfValid();
                };
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(PlatformerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                _machine._rb.velocity = new Vector2(0, _machine._rb.velocity.y);
            }
        }

        [SerializeField] AudioSource _HitAudio;
        class Hit : SimpleState<PlatformerPlayer2D>
        {
            public override void EnterState(PlatformerPlayer2D _machine)
            {
                _machine._HitAudio.Play();
                _machine._anim.CrossFade("Hit", 0f);
                Wait _new = new Wait(1.2f);
                _new.OnTimeOutAction += delegate
                {
                    if(_machine.IsAlive)
                    {
                        if (Mathf.Abs(_machine._HorizontalInput) > 0.5f) _machine._machine.CurrentState = _machine._Run;
                        if (Mathf.Abs(_machine._HorizontalInput) < 0.5f) _machine._machine.CurrentState = _machine._Idle;
                    }
                };
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(PlatformerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                _machine._rb.velocity = new Vector2(0, 0);
            }
        }

        
        [SerializeField] AudioSource _SlideAudio;
        class Slide : SimpleState<PlatformerPlayer2D>
        {
            public override void EnterState(PlatformerPlayer2D _machine)
            {
                _machine._SlideAudio.Play();
                _machine._anim.CrossFade("Slide", 0f);
                _machine._groundParticle.Play();
                base.EnterState(_machine);
            }

            public override void FixedUpdateState(PlatformerPlayer2D _machine)
            {
                base.FixedUpdateState(_machine);
                if(_machine.transform.localScale.x == 1)
                {
                    _machine._rb.velocity = new Vector2(_machine._MoveSpeed, _machine._rb.velocity.y);
                }
                else if(_machine.transform.localScale.x == -1)
                {
                    _machine._rb.velocity = new Vector2(-_machine._MoveSpeed, _machine._rb.velocity.y);
                }
            }

            public override SimpleState<PlatformerPlayer2D> UpdateState(PlatformerPlayer2D _machine)
            {
                if (_machine._SlideAllow == false)
                {
                    if (Mathf.Abs(_machine._HorizontalInput) > 0.5f) return _machine._Run;
                    if (Mathf.Abs(_machine._HorizontalInput) < 0.5f) return _machine._Idle;
                }
                if (_machine._JumpInput == 1) return _machine._Jump;
                return base.UpdateState(_machine);
            }

            public override void ExitState(PlatformerPlayer2D _machine)
            {
                base.ExitState(_machine);
                _machine._SlideAudio.Stop();
                _machine._groundParticle.Stop();
            }
        }
        
    }

}
