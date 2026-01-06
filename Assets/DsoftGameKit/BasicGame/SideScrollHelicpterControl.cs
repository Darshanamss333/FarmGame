using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SideScrollHelicpterControl : MonoBehaviour
    {
        public enum Mode
        {
            Translate,
            Velocity
        }

        [SerializeField]
        Mode ControlMode;
        [SerializeField]
        Vector3Reference JoysticInput;
        [SerializeField]
        FloatReference Speed;
        [SerializeField]
        FloatReference Smooth;

        Vector3 _deltaInput;
        private void Update()
        {
            Translate();
            Velocty();
        }


        void Translate()
        {
            if (ControlMode == Mode.Translate)
            {
                _deltaInput = Vector3.Lerp(_deltaInput, JoysticInput.Value, Time.deltaTime * Smooth.Value);
                transform.Translate(_deltaInput.x * Speed.Value, _deltaInput.y * Speed.Value, 0, Space.World);
            }
        }

        Rigidbody _rb;
        void Velocty()
        {
            if (ControlMode == Mode.Velocity)
            {
                if (!_rb) _rb = GetComponent<Rigidbody>();

                _deltaInput = Vector3.Lerp(_deltaInput, JoysticInput.Value, Time.deltaTime * Smooth.Value);
                _rb.velocity = new Vector3(_deltaInput.x * Speed.Value, _deltaInput.y * Speed.Value, 0);
            }
        }
    }
}
