using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Travel3D
    {
        Travel3DSupporter _Travel3DSuppoter;
        public Travel3D(Transform _transform , List<Vector3> _path, float _speed)
        {
            _Travel3DSuppoter = _transform.gameObject.AddComponent<Travel3DSupporter>();
            _Travel3DSuppoter.SetPath(_path, _speed);
            Play();
        }

        public void Play()
        {
            _Travel3DSuppoter.Play();
        }

        public void Pause()
        {
            _Travel3DSuppoter.Pause();
        }

        public void Clear()
        {
            _Travel3DSuppoter.Clear();
        }

        public bool YLock
        {
            get
            {
                return _Travel3DSuppoter.YLock;
            }
            set
            {
                _Travel3DSuppoter.YLock = value;
            }
        }

        public bool YLookAt
        {
            get
            {
                return _Travel3DSuppoter.YLookAt;
            }
            set
            {
                _Travel3DSuppoter.YLookAt = value;
            }
        }

        public UnityAction OnCompleteAction
        {
            get
            {
                return _Travel3DSuppoter._OnComplete;
            }
            set
            {
                _Travel3DSuppoter._OnComplete = value;
            }
        }
    }
}
