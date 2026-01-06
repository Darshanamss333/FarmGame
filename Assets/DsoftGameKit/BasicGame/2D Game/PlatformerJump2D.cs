using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class PlatformerJump2D : MonoBehaviour
    {
        [SerializeField]
        Rigidbody2D _RB;
        [SerializeField]
        FloatReference _Input;
        [SerializeField]
        FloatReference _Speed;

        private void Update()
        {
            _Input.Value = Input.GetAxis("Jump");
            GroundCheck();
            InputReadyToPressCheck();
        }


        //GroundCheck--------------------------------------------
        //[SerializeField]
        bool Ground;
        [SerializeField]
        float RaycastDistance;
        [SerializeField]
        LayerMask Mask;
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


        //InputReadyToCheck--------------------------------------
        //[SerializeField]
        bool ReadyToPress;
        void InputReadyToPressCheck()
        {
            if(_Jumping)
            {
                ReadyToPress = false;
            }
            else
            {
                if (_Input.Value == 0)
                {
                    ReadyToPress = true;
                }
            }
        }


        //[SerializeField]
        bool _Jumping;
        [SerializeField]
        FloatReference _MaxHight;
        [SerializeField]
        FloatReference _MinHight;
        Vector2 _StartPos;
        private void FixedUpdate()
        {
            if(_Input.Value == 1 && Ground && !_Jumping && ReadyToPress)
            {
                _StartPos = transform.position;
                _Jumping = true;
            }

            if (_Jumping)
            {
                if (Vector2.Distance(_StartPos, transform.position) >= _MaxHight.Value)
                {
                    _Jumping = false;
                }

                if (_RB.velocity.y < 0.1f)
                {
                    _Jumping = false;
                }

                if (_Input.Value == 0)
                {
                    if (Vector2.Distance(_StartPos, transform.position) >= _MinHight.Value)
                    {
                        _Jumping = false;
                    }
                }

                _RB.AddForce(new Vector2(0, _Speed.Value) , ForceMode2D.Impulse);

            }
        }
    }

}
