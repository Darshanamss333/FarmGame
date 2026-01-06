using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AutoSpawner : MonoBehaviour
    {
        [SerializeField]
        bool ShootOutsideTheCamera;
        bool IsVisible;
        private void OnBecameVisible()
        {
            IsVisible = true;
        }
        private void OnBecameInvisible()
        {
            IsVisible = false;
        }

        [SerializeField]
        bool Auto;
        [SerializeField]
        GameObjectReference _Prefab;
        [SerializeField]
        Transform _SpawnPoint;
        public void Spawn()
        {
            GameObject _new = PoolManager._Instance.NewPoolObject(_Prefab.Value);
            _new.transform.position = _SpawnPoint.transform.position;

            SpawnCallBackEvent?.Invoke();
        }

        float _fireRateTang;
        [SerializeField]
        FloatReference _SpawnRate;
        [SerializeField]
        UnityEvent SpawnCallBackEvent;
        void updateFire()
        {
            if (_fireRateTang < _SpawnRate.Value)
            {
                if (_fireRateTang == 0)
                {
                    Spawn();
                }

                _fireRateTang += Time.deltaTime;
            }
            else
            {
                _fireRateTang = 0;
            }
        }


        private void Update()
        {
            if (Auto)
            {
                if (ShootOutsideTheCamera)
                {
                    updateFire();
                }
                else
                {
                    if (IsVisible)
                    {
                        updateFire();
                    }
                }
            }
        }
    }

}