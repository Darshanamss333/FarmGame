using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class GameData : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            [System.Serializable]
            public class Strings
            {
                public string name;
                public string value;
                public string defaultValue;
                public StringVariable scriptable;
            }
            [System.Serializable]
            public class Floats
            {
                public string name;
                public float value;
                public float defaultValue;
                public FloatVariable scriptable;
            }
            [System.Serializable]
            public class Ints
            {
                public string name;
                public int value;
                public int defaultValue;
                public IntVariable scriptable;
            }
            [System.Serializable]
            public class Bools
            {
                public string name;
                public bool value;
                public bool defaultValue;
                public BoolVariable scriptable;
            }

            public List<Strings> _StringList;
            public List<Floats> _FloatList;
            public List<Ints> _IntList;
            public List<Bools> _BoolList;
        }


        public string _SaveFileName;
        public Data _Data;
        public BoolReference _AutoSaveLoad;
        public FloatReference _AutoSaveInterval;

        [ContextMenu("Load")]
        public void Load()
        {
            if(SaveLoad.Exists(_SaveFileName))
            {
                _Data = SaveLoad.Load<Data>(_SaveFileName);
                loadHelper();
            }
            else
            {
                newFile();
                SaveLoad.Save<Data>(_Data, _SaveFileName);
                _Data = SaveLoad.Load<Data>(_SaveFileName);
                loadHelper();
            }
        }

        void newFile()
        {
            for (int i = 0; i < _Data._StringList.Count; i++)
            {
                _Data._StringList[i].value = _Data._StringList[i].defaultValue;
            }

            for (int i = 0; i < _Data._FloatList.Count; i++)
            {
                _Data._FloatList[i].value = _Data._FloatList[i].defaultValue;
            }

            for (int i = 0; i < _Data._IntList.Count; i++)
            {
                _Data._IntList[i].value = _Data._IntList[i].defaultValue;
            }

            for (int i = 0; i < _Data._StringList.Count; i++)
            {
                _Data._BoolList[i].value = _Data._BoolList[i].defaultValue;
            }
        }

        void loadHelper()
        {
            for (int i = 0; i < _Data._StringList.Count; i++)
            {
                _Data._StringList[i].scriptable.Value = _Data._StringList[i].value;
            }

            for (int i = 0; i < _Data._FloatList.Count; i++)
            {
                _Data._FloatList[i].scriptable.Value = _Data._FloatList[i].value;
            }

            for (int i = 0; i < _Data._IntList.Count; i++)
            {
                _Data._IntList[i].scriptable.Value = _Data._IntList[i].value;
            }

            for (int i = 0; i < _Data._StringList.Count; i++)
            {
                _Data._BoolList[i].scriptable.Value = _Data._BoolList[i].value;
            }
        }

        void saveHelper()
        {
            for (int i = 0; i < _Data._StringList.Count; i++)
            {
                _Data._StringList[i].value = _Data._StringList[i].scriptable.Value;
            }

            for (int i = 0; i < _Data._FloatList.Count; i++)
            {
                _Data._FloatList[i].value = _Data._FloatList[i].scriptable.Value;
            }

            for (int i = 0; i < _Data._IntList.Count; i++)
            {
                _Data._IntList[i].value = _Data._IntList[i].scriptable.Value;
            }

            for (int i = 0; i < _Data._StringList.Count; i++)
            {
                _Data._BoolList[i].value = _Data._BoolList[i].scriptable.Value;
            }
        }

        [ContextMenu("Save")]
        public void Save()
        {
            saveHelper();
            SaveLoad.Save<Data>(_Data, _SaveFileName);
        }

        [ContextMenu("DeleteSaveFile")]
        public void DeleteSaveFile()
        {
            if (SaveLoad.Exists(_SaveFileName))
            {
                SaveLoad.Delete(_SaveFileName);
            }
        }


        private void Start()
        {
            if (_AutoSaveLoad.Value) Load();
        }

        float _AutoSaveTang;
        private void Update()
        {
            if(_AutoSaveLoad.Value)
            {
                _AutoSaveTang += Time.deltaTime;
                if(_AutoSaveTang > _AutoSaveInterval.Value)
                {
                    Save();
                    _AutoSaveTang = 0;
                }
            }
        }
    }
}
