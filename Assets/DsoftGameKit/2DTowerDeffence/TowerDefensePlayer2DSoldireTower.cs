using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class TowerDefensePlayer2DSoldireTower : MonoBehaviour
    {
        public FloatReference _SpawnInterval;
        public List<GameObject> _Soldirs;
        private void OnEnable()
        {
            for (int i = 0; i < _Soldirs.Count; i++)
            {
                _Soldirs[i].SetActive(true);
                GameObject _sol = _Soldirs[i];

                _Soldirs[i].GetComponent<BaseCharacter>().OnDieAction += delegate
                {
                    Wait _new = new Wait(_SpawnInterval.Value);
                    _new.OnTimeOutAction += delegate
                    {
                        _sol.transform.position = transform.position;
                        _sol.SetActive(true);
                    };
                };
            }
        }
    }
}
