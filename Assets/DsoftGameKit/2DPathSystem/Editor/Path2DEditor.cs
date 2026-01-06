using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DsoftGameKit
{
    [CustomEditor(typeof(Path2D))]
    public class Path2DEditor : Editor
    {
        Path2D _target;
        private void Awake()
        {
            _target = (Path2D)target;

            if(_target._Waypoints == null)
            {
                _target._Waypoints = new List<Path2D.Waypoint>();
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

        }

        public void OnSceneGUI()
        {
            for (int wi = 0; wi < _target._Waypoints.Count; wi++)
            {
                Path2D.Waypoint waypoint = _target._Waypoints[wi];

                if (waypoint._StartPoint)
                {
                    waypoint._StartPoint.gameObject.name = wi.ToString();
                    Handles.Label(waypoint._StartPoint.position, wi.ToString());


                    waypoint._StartPoint.position = Handles.PositionHandle(waypoint._StartPoint.position, Quaternion.identity);
                }

                for (int pi = 0; pi < _target._Waypoints[wi]._EndPoints.Count; pi++)
                {
                    if(waypoint._StartPoint && waypoint._EndPoints[pi])
                    {
                        Handles.DrawLine(waypoint._StartPoint.position, waypoint._EndPoints[pi].position);
                    }
                }
            }


            if (GUI.changed && !Application.isPlaying)
            {
                EditorUtility.SetDirty(_target);
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);
            }
        }

    }

}
