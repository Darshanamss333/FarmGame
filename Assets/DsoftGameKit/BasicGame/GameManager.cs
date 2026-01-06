using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DsoftGameKit;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour 
{
    public static GameManager _Instance
    {
        get
        {
            if (GameManager.LocalInstance == null)
            {
                GameObject _new = new GameObject();
                _new.name = "GameManager";
                GameManager _Instance = _new.AddComponent<GameManager>();
                LocalInstance = _Instance;
                LocalInstance.Load();
                DontDestroyOnLoad(LocalInstance.gameObject);
                return _Instance;
            }
            else
            {
                return GameManager.LocalInstance;
            }

        }
    }

    public static GameManager LocalInstance;

    [System.Serializable]
    public class DataClass
    {
        public bool _RemoveAds;
        public ResourcesManager.ResPack _Pack;
        public List<string> _OtherSlots;

        public DataClass()
        {
            _Pack = new ResourcesManager.ResPack();
            _OtherSlots = new List<string>();
        }

        int getindex(string _id)
        {
            for (int i = 0; i < _OtherSlots.Count; i++)
            {
                string[] _split = _OtherSlots[i].Split("@");
                if (_split.Length == 2 && _split[0] == _id) return i;
            }

            return -1;
        }

        public string GetOtherSlot(string _id)
        {
            if(getindex(_id) == -1)
            {
                return null;
            }
            else
            {
                return _OtherSlots[getindex(_id)].Split("@")[1];
            }
        }

        public void SetOtherSlot(string _id , string _value)
        {
            if (getindex(_id) == -1)
            {
                _OtherSlots.Add(_id + "@" + _value);
            }
            else
            {
                int _index = getindex(_id);
                _OtherSlots[_index] = _id + "@" + _value;
            }
        }
    }

    public int _LevelCoinCount;


    public DataClass _Data;
    void Load()
    {
        if (SaveLoad.Exists("GameData"))
        {
            _Data = SaveLoad.Load<DataClass>("GameData");
        }
        else
        {
            _Data = new DataClass();
        }
    }

    void Save()
    {
        _OnBeforeSave?.Invoke();
        SaveLoad.Save<DataClass>(_Data, "GameData");
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

    public UnityAction _OnBeforeSave;

    public SetPlayerPosition _startpos;
    public LevelFinishTrigger _endpoint;
    public PlatformerPlayer2D _player;

    internal Action _OnGameplayStart;
    internal Action _OnPlayerInteract;
    internal Action _OnGameplayOver;
    internal Action _OnCommercialBreak;
}
