using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class Travel2DSupporter : MonoBehaviour
    {
        [SerializeField] bool _play;
        [SerializeField] List<Vector3> _path;

        [SerializeField] float _speed;
        [SerializeField] int _index;
        [SerializeField] float _tang;
        [SerializeField] float _maxTang;
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
                            transform.position = Vector3.Lerp(_path[_index], _path[_index + 1], Mathf.InverseLerp(0, _maxTang, _tang));
                        }
                        else
                        {
                            _index += 1;
                            _tang = 0;
                        }
                    }
                    else
                    {
                        _OnComplete?.Invoke();
                        Destroy(this);
                    }


                    if(_deltaPos != transform.position)
                    {
                        Vector3 _dir = transform.position - _deltaPos;
                        if(_dir.x > 0)
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        if(_dir.x < 0)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        _deltaPos = transform.position;
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
