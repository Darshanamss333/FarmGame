using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyInputManager : MonoBehaviour
{
    public static EasyInputManager _Instance
    {
        get
        {
            if (EasyInputManager.LocalInstance == null)
            {
                GameObject _new = new GameObject();
                _new.name = "EasyInputManager";
                EasyInputManager _Instance = _new.AddComponent<EasyInputManager>();
                return _Instance;
            }
            else
            {
                return EasyInputManager.LocalInstance;
            }

        }
    }

    public static EasyInputManager LocalInstance;
    private void Awake()
    {
        if(LocalInstance == null)
        {
            LocalInstance = this;
        }
    }

    [System.Serializable]
    public class InputDataClass
    {
        public List<Vector3> _Joysticks;
        public List<float> _Buttons;
        public InputDataClass()
        {
            _Joysticks = new List<Vector3>();
            _Buttons = new List<float>();
            for (int i = 0; i < 5; i++)
            {
                _Joysticks.Add(new Vector3());
                _Buttons.Add(0);
            }

        }
    }

    public InputDataClass _Data = new InputDataClass();
}
