using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SetNormalUp : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += SetNormal;
        }

        public void SetNormal()
        {
            Ray _ray = new Ray(transform.position + new Vector3(0, 10, 0), Vector3.down);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit, 20))
            {
                transform.rotation = Quaternion.LookRotation(Vector3.Cross(_hit.normal, transform.right), _hit.normal);
            }
        }
    }
}
