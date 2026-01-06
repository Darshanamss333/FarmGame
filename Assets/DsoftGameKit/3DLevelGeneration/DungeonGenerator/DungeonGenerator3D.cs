using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class DungeonGenerator3D : MonoBehaviour
    {
        [SerializeField] Texture2D _Map;
        [SerializeField] GameObjectReference _Midle;
        [SerializeField] GameObjectReference _Walls;
        [SerializeField] GameObjectReference _Conners;
        [SerializeField] GameObjectReference InvertConners;
        [SerializeField] FloatReference _Scale;


        float YRot;
        [SerializeField] float YRotOffSet;
        public void Generate()
        {
            Clear();

            for (int xi = 0; xi < _Map.width; xi++)
            {
                for (int yi = 0; yi < _Map.height; yi++)
                {
                    Vector3 _pos = transform.position + new Vector3(xi, 0, yi) * _Scale.Value;

                    if (PixelExist(xi, yi))
                    {
                        Logic(_pos, xi, yi);
                    }
                }
            }
        }

        void Logic(Vector3 _pos , int x , int y)
        {
            if (IfMidle(x, y))
            {
                Spawn(_Midle.Value, _pos);
            }
            else if (IfConners(x, y))
            {
                Spawn(_Conners.Value, _pos);
            }
            else if (IfWalls(x, y)) 
            {
                Spawn(_Walls.Value, _pos);
            }
            else
            {
                IfInvertConners(x, y);
                Spawn(InvertConners.Value, _pos);
            }
        }

        bool ObjectExist(Vector3 _pos)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).transform.position == _pos)
                {
                    return true;
                }
            }

            return false;
        }

        void Spawn(GameObject _object ,Vector3 _pos)
        {
            GameObject _new = Instantiate(_object);
            _new.transform.position = _pos;
            _new.transform.localScale = Vector3.one * _Scale.Value;
            _new.transform.rotation = Quaternion.Euler(_new.transform.eulerAngles.x, YRot + YRotOffSet, _new.transform.eulerAngles.z);
            _new.transform.parent = transform.Find("Bricks").transform;
        }

        bool PixelExist(int x , int y)
        {
            if(_Map.width > x && _Map.height > y)
            {
                if (_Map.GetPixel(x, y).a > 0)
                {
                    return true;
                }
            }
            return false;
        }

        void Clear()
        {
            if (!transform.Find("Bricks"))
            {
                GameObject _brickParent = new GameObject();
                _brickParent.transform.SetParent(transform);
                _brickParent.name = "Bricks";
            }

            int count = transform.Find("Bricks").childCount;
            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(transform.Find("Bricks").GetChild(0).gameObject);
            }
        }


        bool IfMidle(int x , int y)
        {
            /*
             111
             111
             111
             */
            if(PixelExist(x - 1 , y + 1) && PixelExist(x, y + 1) && PixelExist(x + 1 , y + 1) && PixelExist(x + 1 , y) && PixelExist(x + 1 , y -1) && PixelExist(x , y - 1) && PixelExist(x -1 , y - 1) && PixelExist(x - 1 , y))
            {
                return true;
            }

            return false;
        }

        bool IfWalls(int x , int y)
        {
            /*
             0
            111
             1
            */
            if (PixelExist(x - 1, y) && PixelExist(x + 1, y) && !PixelExist(x, y + 1) && PixelExist(x, y - 1))
            {
                YRot = 0;
                return true;
            }

             /*
             1
            110
             1
            */
            if (PixelExist(x - 1, y) && !PixelExist(x + 1, y) && PixelExist(x, y + 1) && PixelExist(x, y - 1))
            {
                YRot = 90;
                return true;
            }

            /*
             1
            111
             0
            */
            if (PixelExist(x - 1, y) && PixelExist(x + 1, y) && PixelExist(x, y + 1) && !PixelExist(x, y - 1))
            {
                YRot = 180;
                return true;
            }


            /*
             1
            011
             1
            */
            if (!PixelExist(x - 1, y) && PixelExist(x + 1, y) && PixelExist(x, y + 1) && PixelExist(x, y - 1))
            {
                YRot = 270;
                return true;
            }


            return false;
        }

        bool IfConners(int x, int y)
        {
            /*
            00
            01
              1
            */
            if (!PixelExist(x - 1, y) && !PixelExist(x - 1, y + 1) && !PixelExist(x, y + 1) && PixelExist(x + 1, y - 1))
            {
                YRot = 0;
                return true;
            }

            /*
             00
             10
            1
            */
            if (!PixelExist(x + 1, y) && !PixelExist(x + 1, y + 1) && !PixelExist(x, y + 1) && PixelExist(x - 1, y - 1))
            {
                YRot = 90;
                return true;
            }

            /*
           1 
            10
            00
            */
            if (!PixelExist(x + 1, y) && !PixelExist(x + 1, y - 1) && !PixelExist(x, y - 1) && PixelExist(x - 1, y + 1))
            {
                YRot = 180;
                return true;
            }

            /*
              1
            01
            00
           */
            if (!PixelExist(x - 1, y) && !PixelExist(x - 1, y - 1) && !PixelExist(x, y - 1) && PixelExist(x + 1, y + 1))
            {
                YRot = 270;
                return true;
            }

            return false;
        }


        void IfInvertConners(int x, int y)
        {
            if (!PixelExist(x - 1, y + 1) && PixelExist(x, y + 1) && PixelExist(x - 1, y))
            {
                YRot = 0;
            }

            if (PixelExist(x, y + 1) && !PixelExist(x + 1, y + 1) && PixelExist(x + 1, y))
            {
                YRot = 90;
            }

            if (PixelExist(x + 1, y) && !PixelExist(x + 1, y - 1) && PixelExist(x, y - 1))
            {
                YRot = 180;
            }

            if (PixelExist(x - 1, y) && !PixelExist(x , y - 1) && PixelExist(x + 1, y - 1))
            {
                YRot = 270;
            }
        }
    }
}
