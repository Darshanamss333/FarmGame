using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class oldPlatformerPlayer2D : Player
    {
        Rigidbody2D _rb;
        Animator _anim;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            GroundCheck();
            PlatfromCheck();
            FallDownPlatform();
            JumpButtonCheckForReady();
            DoubleJumpAvalableCheck();
            StateControll();
            DashButtonCheckForReady();
            DashAvalableCheck();
        }

        private void FixedUpdate()
        {
            StateControllFixed();
        }

        //State---------------------
        [Header("State--------------------------")]
        [SerializeField] State _States;
        [SerializeField] State _deltaState;
        public enum State
        {
            None,Idle,Run,Jump,DoubleJump,Fall,Attack,Dash,DamageForceBack
        }
        void StateControll()
        {
            switch(_States)
            {
                case State.Idle:
                    Idle();
                    Attack();
                    Dash();
                    break;

                case State.Run:
                    Run();
                    Flip();
                    Attack();
                    Dash();
                    break;

                case State.Jump:
                    Jump();
                    Flip();
                    Attack();
                    Dash();
                    break;

                case State.Fall:
                    Fall();
                    Flip();
                    Attack();
                    Dash();
                    break;

                case State.Attack:
                    Attack();
                    break;

                case State.DamageForceBack:
                    break;

                case State.DoubleJump:
                    Flip();
                    DoubleJump();
                    Dash();
                    break;

                case State.Dash:
                    Dash();
                    break;
            }
        }

        void StateControllFixed()
        {
            switch (_States)
            {
                case State.Idle:
                    IdleFixed();
                    DamageForceBackFixed();
                    break;

                case State.Run:
                    RunFixed();
                    DamageForceBackFixed();
                    break;

                case State.Jump:
                    JumpFixed();
                    RunFixed();
                    DamageForceBackFixed();
                    break;

                case State.Fall:
                    RunFixed();
                    DamageForceBackFixed();
                    break;

                case State.Attack:
                    RunFixed();
                    DamageForceBackFixed();
                    break;

                case State.DamageForceBack:
                    DamageForceBackFixed();
                    break;

                case State.DoubleJump:
                    DoubleJumpFixed();
                    RunFixed();
                    DamageForceBackFixed();
                    break;

                case State.Dash:
                    DashFixed();
                    break;
            }
        }

        //GroundCheck---------------------------------------------------------------------------------------------------
        [Header("GroundCheck--------------------")]
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

        //Move----------------------------------------------------------------------------------------------------------
        [Header("Move----------------------------")]
        [SerializeField] FloatReference _MoveInput;
        [SerializeField] FloatReference _MoveSpeed;
        void RunFixed()
        {
            _rb.velocity = new Vector2(_MoveInput.Value * _MoveSpeed.Value, _rb.velocity.y);
        }
        void Run()
        {
            if (_States != _deltaState)
            {
                _anim.CrossFade("Run", 0.1f);
                _deltaState = _States;
            }

            if (Mathf.Abs(_MoveInput.Value) < 0.5f) _States = State.Idle;
            if (_JumpInput.Value == 1 && _ButtonReadyForNextJump && Ground) _States = State.Jump;
            if (_rb.velocity.y < 0 && !Ground) _States = State.Fall;
            if (_AttackInput.Value == 1) _States = State.Attack;

            if (_DashInput.Value == 1 && _ButtonReadyForNextDash) _States = State.Dash;
        }

        //Flip------------------------------------------------------------------------------------------------------------
        void Flip()
        {
            if (_MoveInput.Value > 0) transform.localScale = new Vector3(1, 1, 1);
            if (_MoveInput.Value < 0) transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jump---------------------------------------------------------------------------------------------------------------
        [Header("Jump----------------------------")]
        [SerializeField] FloatReference _JumpInput;
        [SerializeField] FloatReference _JumpSpeed;
        [SerializeField] FloatReference _MaxJump;
        bool _ButtonReadyForNextJump;
        Vector3 _jumpStartPos;
        void JumpFixed()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _JumpSpeed.Value);
        }
        void Jump()
        {
            if (_States != _deltaState)
            {
                _anim.CrossFade("Jump" , 0.1f);

                _jumpStartPos = transform.position;
                _ButtonReadyForNextJump = false;
                _deltaState = _States;
            }

            if(!Ground)
            {
                float jumpDistance = Vector2.Distance(_jumpStartPos, transform.position);
                if (jumpDistance > _MaxJump.Value) _States = State.Fall;
                if (_rb.velocity.y <= 0) _States = State.Fall;
                if (_JumpInput.Value == 0) _States = State.Fall;
                if (_AttackInput.Value == 1) _States = State.Attack;

                if (_JumpInput.Value == 1 && _ButtonReadyForNextJump && !Ground && _doubleJumpAvalable) _States = State.DoubleJump;

                if (_DashInput.Value == 1 && _ButtonReadyForNextDash) _States = State.Dash;
            }
        }
        void JumpButtonCheckForReady()
        {
            if (_JumpInput.Value == 0 && !_ButtonReadyForNextJump) _ButtonReadyForNextJump = true;
        }

        //Fall------------------------------------------------------------------------------------------------------------------
        //[Header("Fall----------------------------")]
        void Fall()
        {
            if (_States != _deltaState)
            {
                _deltaState = _States;
            }

            if(_rb.velocity.y < 0)
            {
                if(!_anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
                {
                    _anim.CrossFade("Fall" , 0.1f);
                }
            }

            if (Ground && _MoveInput.Value == 0) _States = State.Idle;
            if (Ground && Mathf.Abs(_MoveInput.Value) > 0) _States = State.Run;
            if (_AttackInput.Value == 1) _States = State.Attack;

            if (_JumpInput.Value == 1 && _ButtonReadyForNextJump && !Ground && _doubleJumpAvalable) _States = State.DoubleJump;

            if (_DashInput.Value == 1 && _ButtonReadyForNextDash) _States = State.Dash;
        }

        //DoubleJump----------------------------------------------------------------------------------------------------------
        [Header("DoubleJump----------------------------")]
        bool _doubleJumpAvalable;
        bool _y;
        void DoubleJumpFixed()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _JumpSpeed.Value);
        }
        void DoubleJump()
        {
            if (_States != _deltaState)
            {
                _anim.CrossFade("DoubleJump", 0.1f);

                _y = false;
                _jumpStartPos = transform.position;
                _ButtonReadyForNextJump = false;
                _doubleJumpAvalable = false;
                _deltaState = _States;
            }

            if (!Ground)
            {
                float jumpDistance = Vector2.Distance(_jumpStartPos, transform.position);
                if (jumpDistance > _MaxJump.Value) _States = State.Fall;
                if (_JumpInput.Value == 0) _States = State.Fall;
                if (_AttackInput.Value == 1) _States = State.Attack;

                if (_rb.velocity.y > 0) _y = true;
                if (_rb.velocity.y <= 0 && _y) _States = State.Fall;
            }

        }
        void DoubleJumpAvalableCheck()
        {
            if (Ground && _doubleJumpAvalable == false) _doubleJumpAvalable = true;
        }

        //Dash------------------------------------------------------------------------------------------------------------------
        [Header("Dash----------------------------")]
        [SerializeField] FloatReference _DashInput;
        [SerializeField] FloatReference _DashSpeed;
        [SerializeField] FloatReference _MaxDash;
        float _dashStartPos;
        bool _x;
        void Dash()
        {
            if(_States != _deltaState && _States == State.Dash && _ButtonReadyForNextDash && _DashAvalable)
            {
                _x = false;
                _ButtonReadyForNextDash = false;
                _DashAvalable = false;
                _dashStartPos = transform.position.x;
                _deltaState = _States;
            }

            if(_States == State.Dash)
            {
                float _distance =  Mathf.Abs(_dashStartPos - transform.position.x);
                if (_distance >= _MaxDash.Value) _States = State.Idle;

                if (Mathf.Abs(_rb.velocity.x) > 0) _x = true;
                if (_rb.velocity.x == 0 && Ground && _x) _States = State.Idle;
                if (_rb.velocity.x == 0 && !Ground && _x) _States = State.Fall;
            }
        }
        void DashFixed()
        {
            _rb.velocity = new Vector2(transform.localScale.x * _DashSpeed.Value, 0);

        }

        bool _ButtonReadyForNextDash;
        void DashButtonCheckForReady()
        {
            if (_DashInput.Value == 0 && !_ButtonReadyForNextDash) _ButtonReadyForNextDash = true; 
        }

        bool _DashAvalable;
        void DashAvalableCheck()
        {
            if(Ground && !_DashAvalable)
            {
                _DashAvalable = true;
            }
        }


        //Idle----------------------------------------------------------------------------------------------------------------
        //[Header("Idle----------------------------")]
        void Idle()
        {
            if (_States != _deltaState)
            {
                _anim.CrossFade("Idle",0.1f);
                _deltaState = _States;
            }

            if (Mathf.Abs(_MoveInput.Value) > 0.5f) _States = State.Run;
            if (_JumpInput.Value == 1 && _ButtonReadyForNextJump && Ground) _States = State.Jump;
            if (_rb.velocity.y < 0 && !Ground) _States = State.Fall;
            if (_AttackInput.Value == 1) _States = State.Attack;

            if (_DashInput.Value == 1 && _ButtonReadyForNextDash) _States = State.Dash;
        }
        void IdleFixed()
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        //Attack--------------------------------------------------------------------------------------------------------------
        [Header("Attack----------------------------")]
        [SerializeField] GameObjectReference _DamageBox;
        [SerializeField] FloatReference _AttackInput;
        void Attack()
        {
            /*
            if(_States == State.Attack)
            {
                if (_States != _deltaState)
                {
                    _anim.Play("Attack");
                    _DamageBox.Value.SetActive(true);
                    Wait _new = new Wait(0.3f);
                    _new.OnTimeOutAction += delegate
                    {
                        if (_States == State.Attack)
                        {
                            _DamageBox.Value.SetActive(false);
                            _States = State.Idle;
                        }
                    };

                    _deltaState = _States;
                }
            }
            else
            {
                _DamageBox.Value.SetActive(false);
            }
            */
        }

        //GroundCheck--------------------------------------------------------------------------------------------------------
        [Header("PlatformCheck--------------------")]
        [SerializeField] bool _PlatformGround;
        [SerializeField] float _PlatformRaycastDistance;
        [SerializeField] LayerMask _PlatformMask;
        void PlatfromCheck()
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, _PlatformRaycastDistance, _PlatformMask))
            {
                _PlatformGround = true;
            }
            else
            {
                 _PlatformGround = false;
            }
        }
        //FallDownPlatform--------------------------------------------------------------------------------------------------
        [Header("FallDownPlatform-------------------")]
        [SerializeField] FloatReference _VerticleInput;
        [SerializeField] CapsuleCollider2D _Collider;
        bool _platformFallStart;
        bool _isPlatformFallButtonReady;
        float _platformFallStartPosY;
        void FallDownPlatform()
        {
            if (!_isPlatformFallButtonReady  && _VerticleInput.Value == 0)
            {
                _isPlatformFallButtonReady = true;
            }

            if (_PlatformGround && _VerticleInput.Value == -1 && !_platformFallStart && _isPlatformFallButtonReady)
            {
                _platformFallStart = true;
                _Collider.isTrigger = true;
                _platformFallStartPosY = transform.position.y;
                _isPlatformFallButtonReady = false;
            }

            if (_platformFallStart)
            {
                if((transform.position.y - _platformFallStartPosY) < -0.5f)
                {
                    _platformFallStart = false;
                    _Collider.isTrigger = false;
                }
            }
        }

        //DamageForceBack-------------------------------------------------------------------------------------------------------
        [Header("DamageForceBack-------------------")]
        [SerializeField] FloatReference _DamageForceValue;
        bool _ForceBackAdded;
        void DamageForceBackFixed()
        {
            if(!_ForceBackAdded)
            {
                OnDamageAction += () => _States = State.DamageForceBack;
                _ForceBackAdded = true;
            }
            if(_States == State.DamageForceBack)
            {
                if (_States != _deltaState)
                {
                    _anim.CrossFade("Damage", 0.1f);

                    _rb.velocity = Vector2.zero;
                    _rb.AddForce(new Vector2(transform.localScale.x * -_DamageForceValue.Value, _DamageForceValue.Value), ForceMode2D.Impulse);
                    _deltaState = _States;
                }

                else
                {
                    if (Ground)
                    {
                        Wait _new = new Wait(.2f);
                        _new.OnTimeOutAction += delegate
                        {
                            if (Mathf.Abs(_MoveInput.Value) > 0.5f) _States = State.Run;
                            if (Mathf.Abs(_MoveInput.Value) < 0.5f) _States = State.Idle;
                        };
                    }
                }
            }
        }

    }

}
