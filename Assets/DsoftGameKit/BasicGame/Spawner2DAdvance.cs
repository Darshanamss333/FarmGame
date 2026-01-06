using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Spawner2DAdvance : CommenEvents
    {
        [SerializeField] GameObjectReference _Object;
        [SerializeField] FloatReference _Count;
        [SerializeField] GameObjectReference _SpawnPosition;

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
                _new.transform.position = _SpawnPosition.Value.transform.position;
                _new.SetActive(true);
            }
            
        }
    }
}
