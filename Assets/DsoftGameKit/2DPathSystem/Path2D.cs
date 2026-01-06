using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Path2D : MonoBehaviour
    {
        [System.Serializable]
        public class Waypoint
        {
            public Transform _StartPoint;
            public List<Transform> _EndPoints;

            public Waypoint()
            {
                _EndPoints = new List<Transform>();
            }
        }

        public List<Waypoint> _Waypoints;


        //GetRandomPath---------------------------------------
        [ContextMenu("GetRandomPath")]
        public List<Vector3> GetRandomPath(Transform _self)
        {
            List<Vector3> _result = new List<Vector3>();
            _result.Add(_self.position);
            _result.Add(_Waypoints[0]._StartPoint.position);

            int _index = 0;
            while(_Waypoints[_index]._EndPoints.Count > 0)
            {
                int _randomIndex = (int)Random.Range(0, _Waypoints[_index]._EndPoints.Count);
                _index = getIndexOfWaypoint(_Waypoints[_index]._EndPoints[_randomIndex]);
                _result.Add(_Waypoints[_index]._StartPoint.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)));
            }

            return _result;
        }

        int getIndexOfWaypoint(Transform _input)
        {
            for (int i = 0; i < _Waypoints.Count; i++)
            {
                if(_Waypoints[i]._StartPoint.transform == _input)
                {
                    return i;
                }
            }

            return 0;
        }

        public Transform GetClosesWaypoint(Vector3 _pos)
        {
            int _resultIndex = 0;
            for (int i = 0; i < _Waypoints.Count; i++)
            {
                if(Vector3.Distance(_pos , _Waypoints[i]._StartPoint.transform.position) < Vector3.Distance(_Waypoints[_resultIndex]._StartPoint.transform.position, _pos))
                {
                    _resultIndex = i;
                }
            }

            return _Waypoints[_resultIndex]._StartPoint;
        }
    }
}
