using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] GameObjectReference Object;
        [SerializeField] GameObjectReference TargetObject;
        [SerializeField] FloatReference Smooth;
        private void Update()
        {
            if(TargetObject.Value)
            {
               Object.Value.transform.position = Vector3.Lerp(Object.Value.transform.position, TargetObject.Value.transform.position, Smooth.Value * Time.deltaTime);
            }
        }
    }
}
