using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(MeshHitEffect))]
    [RequireComponent(typeof(BaseCharacter))]
    public class VFXComboOnDie : MonoBehaviour
    {
        void Die()
        {
            if (_Shake) iTween.ShakePosition(this.gameObject, new Vector3(0.5f, 0.5f, 0.5f), 1f);
            if (_meshHitEffect) _meshHitEffect.HitEffect();
            if (_CameraShake && CameraShake._instance) CameraShake._instance.MiniShake(); 
            if (PoolManager._Instance && _ParticleEffectPrefab)
            {
                GameObject _new = PoolManager._Instance.NewPoolObject(_ParticleEffectPrefab);
                _new.transform.position = transform.position + new Vector3(0, 1, 0);
            }
            if (_DisableGameObjectAfterDie) gameObject.SetActive(false);
        }

        MeshHitEffect _meshHitEffect;
        private void OnEnable()
        {
            _meshHitEffect = GetComponent<MeshHitEffect>();
            BaseCharacter _baseCharacter = GetComponent<BaseCharacter>();
            _baseCharacter.OnDieAction += Die;
        }

        public bool _Shake;
        public bool _CameraShake;
        public GameObject _ParticleEffectPrefab;
        public bool _DisableGameObjectAfterDie;
    }
}
