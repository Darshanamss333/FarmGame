using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class PlaySoundEffect : CommenEvents
    {
        [SerializeField] AudioClip _Clip;
        [SerializeField] FloatReference _Valume;
        [SerializeField] bool _OptimizedMode;
        [SerializeField] bool _Loop;
        AudioSource _audioSource;
        public void Play()
        {
            if(_OptimizedMode)
            {
                if (!_audioSource) _audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.playOnAwake = false;
                _audioSource.clip = _Clip;
                _audioSource.volume = _Valume.Value;
                _audioSource.Play();
            }
            else
            {
                notOptimizePlay();
            }
        }

        void notOptimizePlay()
        {
            Wait _wait = new Wait(_Clip.length);
            _wait.Object.name = "PlaySoundEffect";
            AudioSource _source = _wait.Object.AddComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.clip = _Clip;
            _source.volume = _Valume.Value;
            _source.Play();

            if(_Loop) _wait.OnTimeOutAction += () => notOptimizePlay();
        }

        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += Play;
        }
    }
}
