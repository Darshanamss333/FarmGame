using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;

namespace DsoftGameKit
{
    /*
    public class MeshHitEffect : MonoBehaviour
    {
        [SerializeField]
        FloatReference _Interval;
        [SerializeField]
        Material _hitMaterial;
        [SerializeField]
        List<MeshRenderer> _Meshes;
        public void HitEffect()
        {
            //if(!_start)
            _tang = 0;
            _start = true;
        }

        Material _originalMaterial;
        private void Awake()
        {
            if(_Meshes.Count > 1)
            {
                _originalMaterial = _Meshes[0].sharedMaterials[0];
            }
        }

        bool _start;
        float _tang;
        private void Update()
        {
            if(_start)
            {
                if(_tang < _Interval.Value)
                {
                    if(_tang == 0)
                    {
                        SetHitColor();
                    }
                    _tang += Time.deltaTime;
                }
                else
                {
                    SetOriginalColor();
                    _start = false;
                    _tang = 0;
                }
            }
        }

        void SetHitColor()
        {
            for (int i = 0; i < _Meshes.Count; i++)
            {
                _Meshes[i].sharedMaterial = _hitMaterial;
            }
        }


        void SetOriginalColor()
        {
            for (int i = 0; i < _Meshes.Count; i++)
            {
                _Meshes[i].sharedMaterial = _originalMaterial;
            }
        }

    }
    */
}
