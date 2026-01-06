using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DsoftGameKit
{
    public static class SaveLoad
    {
        public static void Save<T>(T type , string name)
        {
            string _dataPath = Application.persistentDataPath + "/" + name + ".json";
            string _dataText = JsonUtility.ToJson(type);
            File.WriteAllText(_dataPath, _dataText);
            Debug.Log("Save complete " + _dataPath);
        }

        public static bool Exists(string name)
        {
            string _dataPath = Application.persistentDataPath + "/" + name + ".json";

            if (File.Exists(_dataPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static T Load<T>(string name)
        {
            string _dataPath = Application.persistentDataPath + "/" + name + ".json";

            string _dataText = File.ReadAllText(_dataPath);
            T resault = JsonUtility.FromJson<T>(_dataText);
            return resault;
        }

        public static void Delete(string name)
        {
            File.Delete(Application.persistentDataPath + "/" + name + ".json");
        }
    }
}
