using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SpriteAnimation2D : MonoBehaviour
    {
        public SpriteRenderer _Renderer;
        public List<Sprite> _Sprites;
        public float _Fps;

        int _currentFrame;
        float _tang;
        private void Update()
        {
            float _maxTang = 1f /_Fps;

            _tang = Mathf.Clamp(_tang + Time.deltaTime, 0, _maxTang);

            if (_tang < _maxTang && _Renderer.sprite != _Sprites[_currentFrame])
            {
                _Renderer.sprite = _Sprites[_currentFrame];
            }

            if(_tang >= _maxTang)
            {
                _currentFrame = (int)Mathf.Repeat(_currentFrame + 1, _Sprites.Count);
                _tang = 0;
            }
        }
    }

}
