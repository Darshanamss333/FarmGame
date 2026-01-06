using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Travel3DSupporter : MonoBehaviour
    {
        [SerializeField] bool _play;
        [SerializeField] List<Vector3> _path;

        [SerializeField] float _speed;
        [SerializeField] int _index;
        [SerializeField] float _tang;
        [SerializeField] float _maxTang;
        [SerializeField] bool _yLock = true;
        [SerializeField] bool _yLookAt = true;
        public UnityAction _OnComplete;
        Vector3 _deltaPos;
        private void Update()
        {
            if(_play)
            {
                if(_path != null)
                {
                    if(_index + 1 < _path.Count)
                    {
                        if (_tang == 0) _maxTang = Vector3.Distance(_path[_index], _path[_index + 1]);

                        if(_tang <= _maxTang)
                        {
                            _tang += Time.deltaTime * _speed;
                            if(_yLock)
                            {
                                transform.position = Vector3.Lerp(_path[_index], _path[_index + 1], Mathf.InverseLerp(0, _maxTang, _tang));
                            }
                            else
                            {
                                transform.position = Vector3.Lerp(new Vector3(_path[_index].x,transform.position.y,_path[_index].z),
                                    new Vector3(_path[_index + 1].x,transform.position.y,_path[_index + 1].z), Mathf.InverseLerp(0, _maxTang, _tang));
                            }
                        }
                        else
                        {
                            _index += 1;
                            _tang = 0;
                        }

                        if (_deltaPos != transform.position && _yLookAt)
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

        public void SetPath(List<Vector3> _pathValue, float _speedValue)
        {
            _path = _pathValue;
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

        public bool YLookAt
        {
            get
            {
                return _yLookAt;
            }
            set
            {
                _yLookAt = value;
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
