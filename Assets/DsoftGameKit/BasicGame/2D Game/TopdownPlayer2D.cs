using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class TopdownPlayer2D : Player
    {
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] Animator _anim;
        [SerializeField] FloatReference _horizontalInput;
        [SerializeField] FloatReference _VerticleInput;
        [SerializeField] FloatReference _Speed;
        private void FixedUpdate()
        {
            if (_horizontalInput.Value == 0 && _VerticleInput.Value == 0)
            {
                _anim.Play("Idle");
            }
            else
            {
                if (_horizontalInput.Value > 0) _anim.transform.localScale = new Vector3(1, 1, 1);
                if (_horizontalInput.Value < 0) _anim.transform.localScale = new Vector3(-1, 1, 1);

                _anim.Play("Run");
            }

            Vector2 _dir = new Vector2(_horizontalInput.Value, _VerticleInput.Value).normalized * _Speed.Value;
            _rb.velocity = _dir;
        }

        private void Update()
        {
            Guns();
        }

        [SerializeField] FloatReference _HLookInput;
        [SerializeField] FloatReference _VLookInput;
        [SerializeField] GameObject _Gun;

        [SerializeField] UnityEvent _OnShoot;
        [SerializeField] UnityEvent _OnReleaseShoot;
        void Guns()
        {
            Vector3 _PlayerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 _dir = (Input.mousePosition - _PlayerScreenPos).normalized;
            _Gun.transform.rotation = Quaternion.LookRotation(_dir, Vector3.forward);

            if(_dir.x > 0)
            {
                _Gun.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _Gun.transform.localScale = new Vector3(-1, 1, 1);
            }


            if (Input.GetMouseButtonDown(0))
            {
                _OnShoot?.Invoke();
            }

            if(Input.GetMouseButtonUp(0))
            {
                _OnReleaseShoot?.Invoke();
            }
        }
    }
}
