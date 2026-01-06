using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Gun : MonoBehaviour
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
        GameObjectReference _BulletPrefab;
        [SerializeField]
        Transform _firePoint;
        public void Shoot()
        {
            GameObject _new = PoolManager._Instance.NewPoolObject(_BulletPrefab.Value);
            _new.transform.position = _firePoint.transform.position;
            _new.transform.rotation = _firePoint.transform.rotation;

            ShootCallBackEvent?.Invoke();
        }

        float _fireRateTang;
        [SerializeField]
        FloatReference _FireRate;
        [SerializeField]
        UnityEvent ShootCallBackEvent;
        void updateFire()
        {
            if (_fireRateTang < _FireRate.Value)
            {
                if (_fireRateTang == 0)
                {
                    Shoot();
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