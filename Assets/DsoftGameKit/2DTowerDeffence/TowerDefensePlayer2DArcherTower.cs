using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Gun))]
    public class TowerDefensePlayer2DArcherTower : MonoBehaviour
    {
        [SerializeField] GameObject _Target;
        [SerializeField] Transform _GunHatch;
        private void Update()
        {
            if(_Target && _Target.active)// && _Target.active)
            {
                _gun.enabled = true;
                Vector3 _dir = _Target.transform.position - _GunHatch.position;
                //_GunHatch.rotation = Quaternion.RotateTowards(_GunHatch.rotation, Quaternion.LookRotation(_dir, -Vector3.forward), Time.time * 100f);
                _GunHatch.rotation = Quaternion.LookRotation(_dir);
            }
            else
            {
                _gun.enabled = false;
            }
        }

        public GameObject Target
        {
            set
            {
                _Target = value;
            }
        }

        public void TargetExit(GameObject _value)
        {
            if (_value == _Target) _Target = null;
        }

        Gun _gun;
        private void Start()
        {
            _gun = GetComponent<Gun>();
        }
    }
}
