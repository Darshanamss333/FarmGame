using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(AudioSource))]
    public class PlaySound : MonoBehaviour
    {
        AudioSource _AudioSource;
        public void _Play(AudioClip _clip)
        {
            if(!_AudioSource)
            {
                _AudioSource = GetComponent<AudioSource>();
                _AudioSource.playOnAwake = false;
            }

            _AudioSource.clip = _clip;
            _AudioSource.Play();
        }
    }
}
