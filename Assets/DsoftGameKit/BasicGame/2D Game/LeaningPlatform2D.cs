using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class LeaningPlatform2D : MonoBehaviour , IPlayerMarioEvents
    {
        [SerializeField] float _maxAngle = 20;
        [SerializeField] GameObject _leanObj;

        bool _ontop;
        public void Refresh(bool _value)
        {
            _ontop = _value;
        }

        float _angle;
        float _ypos;
        private void Update()
        {
            if(_ontop)
            {
                Vector3 _dir = _leanObj.transform.position - Player._Instance.transform.position;
                _angle = _maxAngle * _dir.normalized.x;
                _ypos = -0.4f;
            }
            else
            {
                _angle = 0;
                _ypos = 0;
            }

            _leanObj.transform.rotation = Quaternion.Lerp(_leanObj.transform.rotation, Quaternion.Euler(0, 0, _angle), Time.deltaTime);
            _leanObj.transform.localPosition = Vector3.Lerp(_leanObj.transform.localPosition, new Vector3(0, _ypos, 0), Time.deltaTime * 3);
        }
    }

}
