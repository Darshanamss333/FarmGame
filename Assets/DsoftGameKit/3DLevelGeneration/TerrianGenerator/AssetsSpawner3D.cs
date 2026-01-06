using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class AssetsSpawner3D : MonoBehaviour
    {
        [SerializeField] string _Name;
        [SerializeField] Texture2D _Map;
        [SerializeField] GameObjectReference _Asset;

        [SerializeField] FloatReference _Size;
        [SerializeField] FloatReference _RotY;
        [SerializeField] FloatReference _OffsetPosX;
        [SerializeField] FloatReference _OffsetPosY;
        [SerializeField] FloatReference _OffsetPosZ;
        [SerializeField] BoolReference _LimitCount;
        [SerializeField] FloatReference _SpawnPercentageFrom100;

        Vector3[] _vert = new Vector3[0];
        GameObject _asstesParent;
        public void Generate()
        {
            MeshFilter _filter = GetComponent<MeshFilter>();
            _vert = _filter.sharedMesh.vertices;
            MinMax();

            if (transform.Find(_Name))
            {
                DestroyImmediate(transform.Find(_Name).gameObject);
            }

            _asstesParent = new GameObject();
            _asstesParent.transform.parent = transform;
            _asstesParent.transform.position = Vector3.zero;
            _asstesParent.name = _Name;


            for (int xi = 0; xi < _Map.width; xi++)
            {
                for (int yi = 0; yi < _Map.height; yi++)
                {
                    Color _col = _Map.GetPixel(xi, yi);
                    float _value = (_col.r + _col.g + _col.b) / 3;

                    if (_value > 0.5)
                    {
                        Vector3 _pos = transform.position + new Vector3(Mathf.Lerp(_min, _max, Mathf.InverseLerp(0, _Map.width, xi)) , 0, Mathf.Lerp(_min, _max, Mathf.InverseLerp(0, _Map.height, yi))) * transform.localScale.x;
                        _pos = _pos + new Vector3(0, transform.position.y + 100, 0);
                        _pos = _pos + new Vector3(_OffsetPosX.Value, _OffsetPosY.Value, _OffsetPosZ.Value);
                        Ray _ray = new Ray(_pos, Vector3.down);
                        RaycastHit _hit;

                        
                        if (Physics.Raycast(_ray, out _hit, 200))
                        {
                            if (_hit.collider.gameObject == gameObject)
                            {
                                if(_LimitCount.Value)
                                {
                                    if(_SpawnPercentageFrom100.Value > Random.Range(0,100))
                                    {
                                        Spawn(_hit);
                                    }
                                }
                                else
                                {
                                    Spawn(_hit);
                                }
                            }
                        }
                       
                    }
                }
            }
        }

        private void Spawn(RaycastHit _hit)
        {
            GameObject _new = Instantiate(_Asset.Value);
            _new.transform.position = _hit.point;

            _new.transform.localScale = Vector3.one * _Size.Value;
            _new.transform.rotation = Quaternion.Euler(_new.transform.rotation.eulerAngles.x, _RotY.Value, _new.transform.rotation.eulerAngles.z);
            _new.transform.parent = _asstesParent.transform;
        }

        float _min;
        float _max;
        void MinMax()
        {
            for (int i = 0; i < _vert.Length; i++)
            {
                if (_min > _vert[i].x) _min = _vert[i].x;
                if (_max < _vert[i].x) _max = _vert[i].x;
            }
        }
    }
}
