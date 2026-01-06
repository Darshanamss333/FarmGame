using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{    
    public class MovingPlatform2D : MonoBehaviour
    {
        [SerializeField] Vector3 _StartOffset;
        [SerializeField] Vector3 _EndOffset;
        [SerializeField] float _Speed = 1;

        [SerializeField] GameObject _startObj;
        [SerializeField] GameObject _endObj;
        [SerializeField] GameObject _platformObj;
        [SerializeField] GameObject _lineObj;

        Vector3 _startpos;
        Vector3 _endpos;
        private void Start()
        {
            _startpos = transform.position + _StartOffset;
            _endpos = transform.position + _EndOffset;

            _startObj.transform.position = _startpos;
            _endObj.transform.position = _endpos;
            _lineObj.transform.position = _startpos + ((_endpos - _startpos) * 0.5f);
            SpriteRenderer _lineSpriteRenderr = _lineObj.GetComponent<SpriteRenderer>();
            _lineSpriteRenderr.size = new Vector2(Vector3.Distance(_startpos , _endpos), _lineSpriteRenderr.size.y);
            _lineObj.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, _endpos - _startpos));

            _tang = Mathf.InverseLerp(0, Vector3.Distance(_startpos, _endpos), Vector3.Distance(_startpos , transform.position));
        }

        float _tang;
        float _dir = 1;
        private void Update()
        {
            if(_tang < 1)
            {
                _tang += Time.deltaTime * _Speed;
                if(_dir == 1)
                {
                    _platformObj.transform.position = Vector3.Lerp(_startpos, _endpos, _tang);
                }
                else
                {
                    _platformObj.transform.position = Vector3.Lerp(_startpos, _endpos, 1f - _tang);
                }
            }
            else
            {
                _dir = _dir * -1;
                _tang = 0;
            }
        }
    }

}
