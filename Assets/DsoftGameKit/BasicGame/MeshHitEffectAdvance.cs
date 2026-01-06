using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(BaseCharacter))] 
    public class MeshHitEffectAdvance : CommenEvents
    {
        public Material _hitMaterial;
        public List<SkinnedMeshRenderer> _Meshes;

        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += HitEffect;

            _originalMaterial = _Meshes[0].sharedMaterial;
        }

        public void HitEffect()
        {
            SetHitColor();
            Wait _new = new Wait(0.1f);
            _new.OnTimeOutAction += SetOriginalColor;
        }

        Material _originalMaterial;

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
}
