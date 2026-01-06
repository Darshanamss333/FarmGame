using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ContraStyleGunRotate : MonoBehaviour
    {
        [SerializeField] Animator _Animator;
        [SerializeField] FloatReference _horizantalValue;
        [SerializeField] FloatReference _VirticleValue;
        [SerializeField] bool _SmoothMode;
        [SerializeField] float _SmoothValue = 1;
        private void Update()
        {
            if(_Animator)
            {
                if(!_SmoothMode)
                {
                    if (Mathf.Abs(_horizantalValue.Value) == 0 && _VirticleValue.Value == 1)
                    {
                        _Animator.SetFloat("Dir", 0);
                    }
                    else if (Mathf.Abs(_horizantalValue.Value) == 1 && _VirticleValue.Value == 1)
                    {
                        _Animator.SetFloat("Dir", 0.25f);
                    }
                    else if (Mathf.Abs(_horizantalValue.Value) == 1 && _VirticleValue.Value == -1)
                    {
                        _Animator.SetFloat("Dir", 0.75f);
                    }
                    else if (Mathf.Abs(_horizantalValue.Value) == 0 && _VirticleValue.Value == -1)
                    {
                        _Animator.SetFloat("Dir", 1f);
                    }
                    else
                    {
                        _Animator.SetFloat("Dir", 0.5f);
                    }
                }
                else
                {
                    if (Mathf.Abs(_horizantalValue.Value) == 0 && _VirticleValue.Value == 1)
                    {
                        _Animator.SetFloat("Dir", Mathf.Lerp(_Animator.GetFloat("Dir") , 0 , Time.deltaTime * _SmoothValue));
                    }
                    else if (Mathf.Abs(_horizantalValue.Value) == 1 && _VirticleValue.Value == 1)
                    {
                        _Animator.SetFloat("Dir", Mathf.Lerp(_Animator.GetFloat("Dir"), 0.25f, Time.deltaTime * _SmoothValue));
                    }
                    else if (Mathf.Abs(_horizantalValue.Value) == 1 && _VirticleValue.Value == -1)
                    {
                        _Animator.SetFloat("Dir", Mathf.Lerp(_Animator.GetFloat("Dir"), 0.75f, Time.deltaTime * _SmoothValue));
                    }
                    else if (Mathf.Abs(_horizantalValue.Value) == 0 && _VirticleValue.Value == -1)
                    {
                        _Animator.SetFloat("Dir", Mathf.Lerp(_Animator.GetFloat("Dir"), 1, Time.deltaTime * _SmoothValue));
                    }
                    else
                    {
                        _Animator.SetFloat("Dir", Mathf.Lerp(_Animator.GetFloat("Dir"), 0.5f, Time.deltaTime * _SmoothValue));
                    }
                }
            }
        }
    }
}
