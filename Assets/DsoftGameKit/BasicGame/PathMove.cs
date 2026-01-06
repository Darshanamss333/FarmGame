/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class PathMove : MonoBehaviour 
{
    [SerializeField]
    PathManager _path;
    [SerializeField]
    float _speed;
    [SerializeField]
    bool _IsLookAt;

    int _currentIndex;
    float _tang;
    [SerializeField]
    bool _start;
    private void Update()
    {
        lerpMachine();
    }

    float _distance;
    void lerpMachine()
    {
        if (_path && _start)
        {
            _distance = getDistance();
            if (_tang < _distance)
            {
                learp();
                _tang += Time.deltaTime * _speed;
            }
            else
            {
                _currentIndex = (int)Mathf.Repeat(_currentIndex + 1, _path.waypoints.Length);
                _distance = 1;
                _tang = 0;
            }
        }
    }

    float getDistance()
    {
        Vector3 _0 = _path.waypoints[_currentIndex].transform.position;
        Vector3 _1 = _path.waypoints[(int)Mathf.Repeat(_currentIndex + 1, _path.waypoints.Length)].transform.position;
        return Vector3.Distance(_0, _1);
    }

    Vector3 _p0;
    Vector3 _p1;
    Vector3 _p2;
    Vector3 _p3;
    void getPoints()
    {
        Vector3 _prePoint = _path.waypoints[(int)Mathf.Repeat(_currentIndex - 1, _path.waypoints.Length)].transform.position;
        Vector3 _nextPoint = _path.waypoints[(int)Mathf.Repeat(_currentIndex + 2, _path.waypoints.Length)].transform.position;
        _p0 = _path.waypoints[_currentIndex].transform.position;
        _p3 = _path.waypoints[(int)Mathf.Repeat(_currentIndex + 1, _path.waypoints.Length)].transform.position;
        _p1 = _p0 + (_p3 - _prePoint).normalized * _distance * 0.33f;
        _p2 = _p3 + (_p0 - _nextPoint).normalized * _distance * 0.33f;

    }

    Vector3 _deltaPos;
    void learp()
    {
        if(_tang == 0)
        {
            getPoints();
        }
        else
        {
            float _realTang = Mathf.InverseLerp(0, _distance, _tang);

            Vector3 _0_1 = Vector3.Lerp(_p0, _p1, _realTang);
            Vector3 _1_2 = Vector3.Lerp(_p1, _p2, _realTang);
            Vector3 _2_3 = Vector3.Lerp(_p2, _p3, _realTang);

            Vector3 _pos = Vector3.Lerp(Vector3.Lerp(_0_1, _1_2, _realTang), Vector3.Lerp(_1_2, _2_3, _realTang), _realTang);
            transform.position = _pos;

            if(_IsLookAt)
            {
                if (transform.position != _deltaPos)
                {
                    Vector3 _dir = transform.position - _deltaPos;
                    transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
                    _deltaPos = transform.position;
                }
            }
        }
    }


    public void StartMove()
    {
        _start = true;
    }

    public void StopMove()
    {
        _start = false;
    }

    public void ResetAll()
    {
        StopMove();
        _currentIndex = 0;
        _tang = 0;
    }

    public void SetCurrentPoint(int _index)
    {
        StopMove();
        _currentIndex = (int)Mathf.Repeat(_index, _path.waypoints.Length);
    }
}
*/