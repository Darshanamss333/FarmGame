using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Travel3DCurve
    {
        Travel3DCurveSupporter _Travel3DCurveSuppoter;
        public Travel3DCurve(Transform _transform , List<Vector3> _path, float _speed)
        {
            _Travel3DCurveSuppoter = _transform.gameObject.AddComponent<Travel3DCurveSupporter>();
            _Travel3DCurveSuppoter.SetPath(_path, _speed);
            Play();
        }

        public void Play()
        {
            _Travel3DCurveSuppoter.Play();
        }

        public void Pause()
        {
            _Travel3DCurveSuppoter.Pause();
        }

        public void Clear()
        {
            _Travel3DCurveSuppoter.Clear();
        }

        public bool YLock
        {
            get
            {
                return _Travel3DCurveSuppoter.YLock;
            }
            set
            {
                _Travel3DCurveSuppoter.YLock = value;
            }
        }

        public float Speed
        {
            get
            {
                return _Travel3DCurveSuppoter.Speed;
            }
            set
            {
                _Travel3DCurveSuppoter.Speed = value;
            }
        }

        public UnityAction OnCompleteAction
        {
            get
            {
                return _Travel3DCurveSuppoter._OnComplete;
            }
            set
            {
                _Travel3DCurveSuppoter._OnComplete = value;
            }
        }
    }
}
