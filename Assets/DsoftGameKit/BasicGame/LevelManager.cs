using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DsoftGameKit
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager _Instance
        {
            get
            {
                if (LevelManager.LocalInstance == null)
                {
                    GameObject _new = new GameObject();
                    _new.name = "LevelManager";
                    LevelManager _Instance = _new.AddComponent<LevelManager>();
                    _Instance.Load();
                    LevelManager.LocalInstance = _Instance;
                    DontDestroyOnLoad(_Instance.gameObject);
                    return _Instance;
                }
                else
                {
                    return LevelManager.LocalInstance;
                }

            }
        }

        public static LevelManager LocalInstance;

        [System.Serializable]
        public class DataClass
        {
            public int _LevelNumber;
            public int _DeltaLevelNumber;
            public List<DataStripClass> _LevelList;

            public DataClass()
            {
                _LevelList = new List<DataStripClass>();
            }

            [System.Serializable]
            public class DataStripClass
            {
                public bool _Unlocked;
                public int _Stars;
            }

            public DataStripClass Get(int _index)
            {
                if (_LevelList == null)
                {
                    _LevelList = new List<DataStripClass>();
                    DataStripClass _new = new DataStripClass();
                    _new._Unlocked = true;
                    _LevelList.Add(_new);
                }

                int _count = (_index + 1) - _LevelList.Count;
                for (int i = 0; i < _count; i++)
                {
                    _LevelList.Add(new DataStripClass());
                }

                _LevelList[0]._Unlocked = true;
                return _LevelList[_index];
            }
        }


        public DataClass _Data;
        public int SelectedLevel
        {
            get
            {
                return _Data._LevelNumber;
            }
            set
            {
                _Data._LevelNumber = value;
            }
        }

        public void CompleteCurrentLevelAndUnlockNext()
        {
            _Data.Get(_Data._LevelNumber)._Stars = 3;
            _Data.Get(_Data._LevelNumber + 1)._Unlocked = true;
            _Data._LevelNumber++;
        }

        void Load()
        {
            if (SaveLoad.Exists("LevelData"))
            {
                _Data = SaveLoad.Load<DataClass>("LevelData");
            }
            else
            {
                _Data = new DataClass();
            }
        }

        void Save()
        {
            SaveLoad.Save<DataClass>(_Data, "LevelData");
        }


        float _tang;
        private void Update()
        {
            if (_tang < 10)
            {
                _tang += Time.deltaTime;
            }
            else
            {
                Save();
                _tang = 0;
            }
        }
    }

}
