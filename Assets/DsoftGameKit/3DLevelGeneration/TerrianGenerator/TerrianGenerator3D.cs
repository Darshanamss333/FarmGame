using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class TerrianGenerator3D : MonoBehaviour
    {
        [SerializeField] Texture2D _Map;
        [SerializeField] FloatReference _Hight;
        [SerializeField] Mesh _SampleMesh;
        [SerializeField] bool _Collider;


        Vector3[] _vert = new Vector3[0];
        public void Generate()
        {
            MeshFilter _filter = GetComponent<MeshFilter>();
            Mesh _newmesh = new Mesh();
            _vert = _SampleMesh.vertices;
            MinMax();
            SetHight();
            _newmesh.vertices = _vert;
            _newmesh.triangles = _SampleMesh.triangles;
            _newmesh.uv = _SampleMesh.uv;
            _filter.mesh = _newmesh;

            if(_Collider)
            {
                if(GetComponent<MeshCollider>() != null)
                {
                    MeshCollider _col = GetComponent<MeshCollider>();
                    DestroyImmediate(_col);
                }

                gameObject.AddComponent<MeshCollider>();
            }
        }

        void SetHight()
        {
            for (int i = 0; i < _vert.Length; i++)
            {
                Color _col = _Map.GetPixel((int)Mathf.Lerp(_Map.width,0, Mathf.InverseLerp(_min, _max, _vert[i].x)), (int)Mathf.Lerp(0, _Map.height, Mathf.InverseLerp(_min, _max, _vert[i].y)));

                float _value = (_col.r + _col.g + _col.b) / 3;
                _vert[i].z = _value * _Hight.Value;
            }
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
