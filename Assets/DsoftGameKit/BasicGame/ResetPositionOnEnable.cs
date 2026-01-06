using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ResetPositionOnEnable : MonoBehaviour
    {
        Vector3 _startPos;
        bool _posDone;
        private void OnEnable()
        {
            if (!_posDone)
            {
                _startPos = transform.position;
                _posDone = true;
            }
            transform.position = _startPos;
        }
    }
}
