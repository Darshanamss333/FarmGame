using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Travel3DCurveSupporter : MonoBehaviour
    {
        [SerializeField] bool _play;
        [SerializeField] List<Vector3> _path;

        [SerializeField] float _speed;
        [SerializeField] int _index;
        [SerializeField] float _tang;
        [SerializeField] float _maxTang;
        [SerializeField] bool _yLock = true;
        public UnityAction _OnComplete;
        Vector3 _deltaPos;
        private void Update()
        {
            if(_play)
            {
                if(_path != null)
                {
                    if(_index + 3 < _path.Count)
                    {
                        if (_tang == 0) _maxTang = Vector3.Distance(_path[_index], _path[_index + 1]) + Vector3.Distance(_path[_index + 1], _path[_index + 2]) + Vector3.Distance(_path[_index + 2], _path[_index + 3]);

                        if(_tang <= _maxTang)
                        {
                            _tang += Time.deltaTime * _speed;
                            float _realtang = Mathf.InverseLerp(0, _maxTang, _tang);

                            Vector3 _1 = Vector3.Lerp(_path[_index], _path[_index + 1], _realtang);
                            Vector3 _2 = Vector3.Lerp(_path[_index + 1], _path[_index + 2], _realtang);
                            Vector3 _3 = Vector3.Lerp(_path[_index + 2], _path[_index + 3], _realtang);

                            Vector3 _4 = Vector3.Lerp(_1, _2, _realtang);
                            Vector3 _5 = Vector3.Lerp(_2, _3, _realtang);
                            Vector3 _6 = Vector3.Lerp(_4, _5, _realtang);

                            if (_yLock)
                            {

                                transform.position = _6;
                            }
                            else
                            {
                                transform.position = new Vector3(_6.x, transform.position.y, _6.z);
                            }
                        }
                        else
                        {
                            _index += 3;
                            _tang = 0;
                        }

                        if (_deltaPos != transform.position)
                        {
                            Vector3 _dir = (transform.position - _deltaPos).normalized;
                            _dir.y = 0;
                            if(_dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
                            _deltaPos = transform.position;
                        }
                    }
                    else
                    {
                        _OnComplete?.Invoke();
                        Destroy(this);
                    }
                }
            }
        }

        List<Vector3> CalculatePathPoints(List<Vector3> _path)
        {
            List<Vector3> _resault = new List<Vector3>();
            for (int i = 0; i < _path.Count; i++)
            {
                if(i < _path.Count - 1)
                {
                    float _distance = Vector3.Distance(_path[i], _path[i + 1]);
                    Vector3 _nextPoint = _path[(int)Mathf.Repeat(i + 2, _path.Count)];
                    Vector3 _prevPoint = _path[(int)Mathf.Repeat(i - 1, _path.Count)];

                    _resault.Add(_path[i]);
                    _resault.Add(_path[i] + ((_path[i + 1] - _prevPoint).normalized * _distance * 0.33f));
                    _resault.Add(_path[i +1] + ((_path[i] - _nextPoint).normalized * _distance * 0.33f));
                }

                if (i == _path.Count - 1)
                {
                    _resault.Add(_path[i]);
                }
            }

            return _resault;
        }

        public void SetPath(List<Vector3> _pathValue, float _speedValue)
        {
            _path = CalculatePathPoints(_pathValue);
            _speed = _speedValue;
        }

        public void Play()
        {
            _play = true;
        }

        public void Pause()
        {
            _play = false;
        }

        public bool YLock
        {
            get
            {
                return _yLock;
            }
            set
            {
                _yLock = value;
            }
        }

        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        private void OnDisable()
        {
            Destroy(this);
        }

        public void Clear()
        {
            Destroy(this);
        }
    }
}
