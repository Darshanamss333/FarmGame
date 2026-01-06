using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(PlaySound))]
    [RequireComponent(typeof(MeshHitEffect))]
    [RequireComponent(typeof(BaseCharacter))]
    public class VFXComboOnDamage : MonoBehaviour
    {
        void Damage()
        {
            if (_soundPlayer && _AudioClip) _soundPlayer._Play(_AudioClip);
            if (_Shake) iTween.ShakePosition(this.gameObject, new Vector3(0.5f, 0, 0.5f), 0.5f);
            if (_meshHitEffect) _meshHitEffect.HitEffect();
            if (_CameraShake && CameraShake._instance) CameraShake._instance.MiniShake();
        }

        PlaySound _soundPlayer;
        MeshHitEffect _meshHitEffect;
        private void OnEnable()
        {
            _soundPlayer = GetComponent<PlaySound>();
            _meshHitEffect = GetComponent<MeshHitEffect>();
            BaseCharacter _baseCharacter = GetComponent<BaseCharacter>();
            _baseCharacter.OnDamageAction += Damage;
        }

        public AudioClip _AudioClip;
        public bool _Shake;
        public bool _CameraShake;
    }
}
