using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public static class AppManager
    {
        public static GameManager.DataClass Data
        {
            get
            {
                return GameManager._Instance._Data;
            }
        }

        public static void RefreshAllTiles()
        {
            foreach (Tiles item in AllTiles)
            {
                item.Refresh();
            }
        }

        public static Farmer Farmer
        {
            get
            {
                return GameObject.FindFirstObjectByType<Farmer>();
            }
        }

        public static float HorisonthalTileGap
        {
            get
            {
                return 10;
            }
        }

        public static float VerticleTileGap
        {
            get
            {
                return 9;
            }
        }

        public static GameObject TileParent
        {
            get
            {
                return GameObject.Find("TilesParent");
            }
        }

        public static List<Vector3> AllTileDirections()
        {
            List<Vector3> _res = new List<Vector3>();
            _res.Add(new Vector3(HorisonthalTileGap * 0.5f, 0, VerticleTileGap));
            _res.Add(new Vector3(HorisonthalTileGap, 0, 0));
            _res.Add(new Vector3(HorisonthalTileGap * 0.5f, 0, -VerticleTileGap));
            _res.Add(new Vector3(-HorisonthalTileGap * 0.5f, 0, -VerticleTileGap));
            _res.Add(new Vector3(-HorisonthalTileGap, 0, 0));
            _res.Add(new Vector3(-HorisonthalTileGap * 0.5f, 0, VerticleTileGap));
            return _res;
        }

        public static List<Tiles> AllTiles
        {
            get
            {
                List<Tiles> _all = new List<Tiles>();
                for (int i = 0; i < TileParent.transform.childCount; i++)
                {
                    _all.Add(TileParent.transform.GetChild(i).GetComponent<Tiles>());
                }

                return _all;
            }
        }

        public static List<Tiles> GetNaigberTile(Tiles _my)
        {
            List<Tiles> _res = new List<Tiles>();

            for (int i = 0; i < AllTileDirections().Count; i++)
            {
                Tiles _current = null;
                foreach (var item in AllTiles)
                {
                    if (item.transform.position == _my.transform.position + AllTileDirections()[i]) _current = item;
                }
                _res.Add(_current);
            }
            return _res;
        }
    }
}
