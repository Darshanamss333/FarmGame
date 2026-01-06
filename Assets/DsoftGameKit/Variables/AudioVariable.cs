using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [CreateAssetMenu(fileName = "newAudioEvent", menuName = "DsoftGameKit/AudioEvent")]
    public class AudioVariable : ScriptableObject
    {
        public List <AudioClip> List;
        [Range(0,1)]
        public float MinVolume = 1;
        [Range(0, 1)]
        public float MaxVolume = 1;

        [Range(-3, 3)]
        public float MinPitch = 1;
        [Range(-3, 3)]
        public float MaxPitch = 1;

        public void Play()
        {
            AudioClip _clip = PickClip();
            Wait _wait = new Wait(_clip.length + 2);
            _wait.Object.name = "AudioEvent";
            AudioSource _source = _wait.Object.AddComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.clip = PickClip();
            _source.volume = PickVolume();
            _source.pitch = PickPitch();
            _source.Play();
        }

        AudioClip PickClip()
        {
            return List[Random.Range(0, List.Count)];
        }

        float PickVolume()
        {
            return Random.Range(MinVolume, MaxVolume);
        }

        float PickPitch()
        {
            return Random.Range(MinPitch, MaxPitch);
        }
    }
}

