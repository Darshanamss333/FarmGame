using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class EndlessRunner2DBackgroundSpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject _Player;
        [SerializeField] List<GameObject> _PrefabList;

        int _prespawnNumber = 3;

        private void Start()
        {
            for (int i = 0; i < _prespawnNumber; i++)
            {
                Spawn();
            }
        }

        float _tang;
        private void Update()
        {
            _tang += Time.deltaTime;
            if(_tang > 1)
            {
                if (Vector3.Distance(_Player.transform.position, GetLast.transform.position) < 100)
                {
                    Spawn();
                }

                if (Vector3.Distance(_Player.transform.position, GetFisrt.transform.position) > 100)
                {
                    DestroyFist();
                }
                _tang = 0;
            }
        }

        void Spawn()
        {
            GameObject _prefab = _PrefabList[Random.Range(0, _PrefabList.Count)];
            GameObject _size = _prefab.transform.Find("size").gameObject;

            GameObject _last = GetLast;
            if(_last == null)
            {
                GameObject _new = Instantiate(_prefab, transform);
                _new.transform.localPosition = Vector3.zero;
            }
            else
            {
                GameObject _new = Instantiate(_prefab, transform);
                _new.transform.localPosition = _last.transform.localPosition + new Vector3(_size.transform.localScale.x, 0, 0);
            }
        }

        void DestroyFist()
        {
            GameObject _fist = GetFisrt;
            if (_fist != null) Destroy(_fist);
        }

        GameObject GetLast
        {
            get
            {
                if(transform.childCount > 0)
                {
                    return transform.GetChild(transform.childCount - 1).gameObject;
                }
                else
                {
                    return null;
                }
            }
        }

        GameObject GetFisrt
        {
            get
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
