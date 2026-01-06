using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class FollowObjectAdvance : MonoBehaviour
    {
        [SerializeField] GameObjectReference Object;
        [SerializeField] GameObjectReference TargetObject;
        [SerializeField] Vector3Reference Offset;
        [SerializeField] FloatReference Smooth;
        private void Update()
        {
            if(TargetObject.Value)
            {
               Object.Value.transform.position = Vector3.Lerp(Object.Value.transform.position, TargetObject.Value.transform.position + Offset.Value, Smooth.Value * Time.deltaTime);
            }
        }
    }
}
