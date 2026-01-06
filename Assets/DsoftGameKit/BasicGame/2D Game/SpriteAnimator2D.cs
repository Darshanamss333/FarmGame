using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SpriteAnimator2D : MonoBehaviour
    {
        [System.Serializable]
        public class Animation
        {
            public string _Name;
            public List<Sprite> _Sprites;
            public float _Fps;
        }

        [SerializeField] SpriteRenderer _Renderer;
        public IntReference _CurrentAnimationIndex;
        [SerializeField] List<Animation> _Animations;

        int _deltaCurrentAnimation;
        int _currentFrame;
        float _tang;
        private void Update()
        {
            if (_deltaCurrentAnimation != _CurrentAnimationIndex.Value)
            {
                _currentFrame = 0;
                _tang = 0;
                _deltaCurrentAnimation = _CurrentAnimationIndex.Value;
            }

            float _maxTang = 1f / _Animations[_CurrentAnimationIndex.Value]._Fps;

            if (_tang < _maxTang && _Renderer.sprite != _Animations[_CurrentAnimationIndex.Value]._Sprites[_currentFrame])
            {
                _Renderer.sprite = _Animations[_CurrentAnimationIndex.Value]._Sprites[_currentFrame];
            }

            _tang = Mathf.Clamp(_tang + Time.deltaTime, 0, _maxTang);

            if (_tang >= _maxTang)
            {
                _currentFrame = (int)Mathf.Repeat(_currentFrame + 1, _Animations[_CurrentAnimationIndex.Value]._Sprites.Count);
                _tang = 0;
            }

        }
    }

}
