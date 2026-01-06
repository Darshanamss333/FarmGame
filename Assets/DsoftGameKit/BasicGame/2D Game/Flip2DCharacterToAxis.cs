using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Flip2DCharacterToAxis : MonoBehaviour
    {
        [SerializeField] bool Flip;
        [SerializeField] GameObjectReference _Object;
        [SerializeField] FloatReference _Axis;
        private void Update()
        {

            if (Flip)
            {
                if (_Axis.Value > 0)
                {
                    _Object.Value.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }

                if (_Axis.Value < 0)
                {
                    _Object.Value.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                if (_Axis.Value > 0)
                {
                    _Object.Value.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }

                if (_Axis.Value < 0)
                {
                    _Object.Value.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
            }

        }
    }
}
