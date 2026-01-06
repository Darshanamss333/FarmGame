using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Spawner2D : CommenEvents
    {
        [SerializeField] GameObjectReference _Object;
        [SerializeField] FloatReference _Count;
        [SerializeField] FloatReference _SpawnDistance;

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
                _new.SetActive(false);
                float _x = Random.Range(-1f, 1f);
                float _y = Random.Range(-1f, 1f);
                _new.transform.position = transform.position + (new Vector3(_x, _y, transform.position.z) * _SpawnDistance.Value);
                _new.SetActive(true);
            }
            
        }
    }
}
