using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Flip2DCharacterToMoveDir : MonoBehaviour
    {
        [SerializeField] bool Flip;
        Vector3 _deltaPos;
        private void Update()
        {
            if(_deltaPos != transform.position)
            {
                Vector3 _dir = (transform.position - _deltaPos).normalized;

                if (Flip)
                {
                    if (_dir.x > 0)
                    {
                        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    }

                    if (_dir.x < 0)
                    {
                        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    }
                }
                else
                {
                    if (_dir.x > 0)
                    {
                        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    }

                    if (_dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    }
                }

                _deltaPos = transform.position;
            }
 
        }
    }
}
