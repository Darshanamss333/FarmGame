using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class UiPointsBar : MonoBehaviour
    {
        [SerializeField] GameObjectReference _PointPrefab;
        [SerializeField] FloatReference _CurrentPoints;
        [SerializeField] Vector3 _Offset;


        float _deltaCurrent;
        private void Update()
        {
            if(_CurrentPoints.Value != _deltaCurrent)
            {
                UpdatePoints();
                _deltaCurrent = _CurrentPoints.Value;
            }
        }

        void UpdatePoints()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }


            for (int i = 0; i < _CurrentPoints.Value; i++)
            {
                GameObject _new = Instantiate(_PointPrefab.Value);
                _new.transform.SetParent(transform);
                _new.transform.localScale = Vector3.one;
                _new.transform.localPosition = Vector3.zero;
                _new.transform.localPosition = _Offset * i;
            }
        }
    }
}
