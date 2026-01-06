using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveAllChildsOverTime : MonoBehaviour
    {

        [SerializeField] FloatReference _Delay;

        bool _playing;
        public void Play()
        {
            if (!_playing)
            {
                _currentTime = 0;
                _playing = true;
            }
        }

        float _currentTime;
        int _currentIndex;
        private void Update()
        {
            if (_playing)
            {
                _currentTime = _currentTime + Time.deltaTime;

                if (_currentTime >= _Delay.Value)
                {
                    transform.GetChild(_currentIndex).gameObject.SetActive(true);
                    _playing = false;

                    if(_currentIndex + 1 < transform.childCount)
                    {
                        _currentIndex += 1;
                        Play();
                    }
                }
            }
        }


        private void OnEnable()
        {
            Play();
        }
    }
}
