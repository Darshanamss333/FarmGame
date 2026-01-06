using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Travel2D
    {
        Travel2DSupporter _Travel2DSuppoter;
        public Travel2D(Transform _transform , List<Vector3> _path, float _speed)
        {
            _Travel2DSuppoter = _transform.gameObject.AddComponent<Travel2DSupporter>();
            _Travel2DSuppoter.SetPath(_path, _speed);
            Play();
        }

        public void Play()
        {
            _Travel2DSuppoter.Play();
        }

        public void Pause()
        {
            _Travel2DSuppoter.Pause();
        }

        public void Clear()
        {
            _Travel2DSuppoter.Clear();
        }

        public UnityAction OnCompleteAction
        {
            get
            {
                return _Travel2DSuppoter._OnComplete;
            }
            set
            {
                _Travel2DSuppoter._OnComplete = value;
            }
        }
    }
}
