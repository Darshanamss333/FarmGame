using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Instantiate : MonoBehaviour
    {
        GameObject _lastSpawnObject;
        public void _InstantiateObject(GameObject _object)
        {
            _lastSpawnObject = Instantiate(_object);
        }

        public void _InstantiatePoolObject(GameObject _object)
        {
            _lastSpawnObject = PoolManager._Instance.NewPoolObject(_object);
        }

        public void _InstantiatePoolObject(GameObjectVariable _object)
        {
            _lastSpawnObject = PoolManager._Instance.NewPoolObject(_object.GetSet);
        }

        public void _InstantiateRandomObjectInList(GameObjectListVariable _objectList)
        {
            _InstantiatePoolObject(_objectList.Value[Random.Range(0, _objectList.Value.Count)]);
        }

        public void _SetPosition(Transform _transform)
        {
            _lastSpawnObject.transform.position = _transform.position; 
        }

        public void _SetRandomXYPosition(Transform _transform)
        {
            _lastSpawnObject.transform.position = _transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }

        public void _SetRotation(Transform _transform)
        {
            _lastSpawnObject.transform.rotation = _transform.rotation;
        }

        public void _SetParent(Transform _transform)
        {
            _lastSpawnObject.transform.parent = _transform;
        }
    }
}