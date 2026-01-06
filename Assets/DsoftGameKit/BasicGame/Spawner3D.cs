using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Spawner3D : CommenEvents
    {
        [SerializeField] GameObjectReference _Object;
        [SerializeField] FloatReference _Count;
        [SerializeField] Vector3Reference _Offset;
        [SerializeField] bool _RaycastYPos = true;
        [SerializeField] LayerMask _Mask;

        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += Spawn;
        }

        public void Spawn()
        {
            for (int i = 0; i < _Count.Value; i++)
            {
                GameObject _new = PoolManager._Instance.NewPoolObject(_Object.Value);
                float _y = 0;

                if(_RaycastYPos)
                {
                    Ray _ray = new Ray(transform.position + new Vector3(0, 10, 0), Vector3.down);
                    RaycastHit _hit;
                    if (Physics.Raycast(_ray, out _hit, 50, _Mask))
                    {
                        _y = _hit.point.y;
                    }
                    else
                    {
                        _y = transform.position.y;
                    }
                }
                else
                {
                    _y = transform.position.y;
                }


                _new.transform.position = new Vector3(transform.position.x, _y, transform.position.z) + new Vector3(Random.Range(-_Offset.Value.x, _Offset.Value.x), Random.Range(-_Offset.Value.y, _Offset.Value.y), Random.Range(-_Offset.Value.z, _Offset.Value.z));

                _new.SetActive(true);
            }
            
        }
    }
}
