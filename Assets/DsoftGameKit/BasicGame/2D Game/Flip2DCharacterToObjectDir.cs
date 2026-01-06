using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Flip2DCharacterToObjectDir : MonoBehaviour
    {
        [SerializeField] bool Flip;
        [SerializeField] GameObjectReference _Object;
        private void Update()
        {
            if(_Object.Value)
            {
                Vector3 _dir = (transform.position - _Object.Value.transform.position).normalized;

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
            }
 
        }
    }
}
