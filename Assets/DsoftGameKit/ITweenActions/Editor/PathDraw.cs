using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DsoftGameKit
{
    public class PathDraw
    {
        public static Vector3[] DrawScenePath(Vector3[]  _path , bool _closePath)
        {
            if(_path.Length > 0)
            {
                for (int i = 0; i < _path.Length; i++)
                {
                    _path[i] = Handles.PositionHandle(_path[i], Quaternion.identity);
                    Handles.color = Color.white;
                    Handles.DrawWireCube(_path[i], Vector3.one * 3);
                    Handles.Label(_path[i], "Point " + i.ToString());

                    Vector3 _nextPoint = Vector3.zero;
                    Vector3 _prevPoint = Vector3.zero;
                    Vector3 _p0 = Vector3.zero;
                    Vector3 _p3 = Vector3.zero;
                    Vector3 _p1 = Vector3.zero;
                    Vector3 _p2 = Vector3.zero;
                    float _distance = 0;
                    if (_closePath)
                    {
                        _nextPoint = _path[(int)Mathf.Repeat(i + 2, _path.Length)];
                        _prevPoint = _path[(int)Mathf.Repeat(i - 1, _path.Length)];

                        _p0 = _path[i];
                        _p3 = _path[(int)Mathf.Repeat(i + 1, _path.Length)];
                        _distance = Vector3.Distance(_p0, _p3);
                        _p1 = _p0 + ((_p3 - _prevPoint).normalized * _distance * 0.33f);
                        _p2 = _p3 + ((_p0 - _nextPoint).normalized * _distance * 0.33f);
                    }
                    else
                    {
                        _nextPoint = _path[Mathf.Clamp(i + 2, 0, _path.Length - 1)];
                        _prevPoint = _path[Mathf.Clamp(i - 1, 0, _path.Length - 1)];

                        _p0 = _path[i];
                        _p3 = _path[Mathf.Clamp(i + 1, 0, _path.Length - 1)];
                        _distance = Vector3.Distance(_p0, _p3);
                        _p1 = _p0 + ((_p3 - _prevPoint).normalized * _distance * 0.33f);
                        _p2 = _p3 + ((_p0 - _nextPoint).normalized * _distance * 0.33f);
                    }

                    Handles.DrawBezier(_p0, _p3, _p1, _p2, Color.white, null, 1);

                    Handles.color = Color.green;
                    Vector3 _pos = Vector3.Lerp(Vector3.Lerp(Vector3.Lerp(_p0, _p1, 0.5f), Vector3.Lerp(_p1, _p2, 0.5f), 0.5f),
                        Vector3.Lerp(Vector3.Lerp(_p1, _p2, 0.5f), Vector3.Lerp(_p2, _p3, 0.5f), 0.5f), 0.5f);
                    if (Handles.Button(_pos, Quaternion.identity, 2, 2, Handles.SphereHandleCap))
                    {
                        List<Vector3> _deltaPoints = new List<Vector3>(_path);
                        int _index = (int)Mathf.Repeat(i + 1, _path.Length);
                        _deltaPoints.Insert(_index, _pos);
                        _path = _deltaPoints.ToArray();
                    }

                    Handles.color = Color.red;
                    if (Handles.Button(_path[i] + new Vector3(-2, -2, 0), Quaternion.identity, 2, 2, Handles.SphereHandleCap))
                    {
                        List<Vector3> _deltaPoints = new List<Vector3>(_path);
                        _deltaPoints.RemoveAt(i);
                        _path = _deltaPoints.ToArray();
                    }
                }
            }
            return _path;
        }

        public static Vector3[] DrawInspectorPath(Vector3[] _path, bool _showWayponts , GameObject _parentGameobject)
        {
            if(_path != null && _path.Length > 0)
            {
                if (_showWayponts)
                {
                    if (GUILayout.Button("ResetPosition"))
                    {
                        List<Vector3> _deltaPoints = new List<Vector3>(_path);

                        for (int i = 0; i < _path.Length; i++)
                        {
                            if (i == 0)
                            {
                                _deltaPoints[i] = _parentGameobject.transform.position;
                            }
                            else
                            {
                                _deltaPoints[i] = _deltaPoints[i - 1] + (_path[i] - _path[i - 1]);
                            }
                        }
                        _path = _deltaPoints.ToArray();
                    }

                    for (int i = 0; i < _path.Length; i++)
                    {
                        _path[i] = EditorGUILayout.Vector3Field("Point " + i.ToString(), _path[i]);

                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button("+"))
                        {
                            List<Vector3> _deltaPoints = new List<Vector3>(_path);
                            int _index = (int)Mathf.Repeat(i + 1, _path.Length);
                            Vector3 _pos = _path[i] + (_path[(int)Mathf.Repeat(i + 1, _path.Length)] - _path[i]) * 0.5f;
                            _deltaPoints.Insert(_index, _pos);
                            _path = _deltaPoints.ToArray();
                        }

                        if (GUILayout.Button("-"))
                        {
                            List<Vector3> _deltaPoints = new List<Vector3>(_path);
                            _deltaPoints.RemoveAt(i);
                            _path = _deltaPoints.ToArray();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            else
            {
                if (GUILayout.Button("CreatePath"))
                {
                    _path = new Vector3[2];
                    _path[0] = _parentGameobject.transform.position;
                    _path[1] = _parentGameobject.transform.position + new Vector3(0, 0, 10);
                }
            }

            return _path;
        }
    }
}
