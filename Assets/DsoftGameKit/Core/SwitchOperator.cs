using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class SwitchOperator : MonoBehaviour
    {
        [SerializeField]
        IntReference Index;

        [System.Serializable]
        class Switch
        {
            public UnityEvent If;
        }

        [SerializeField]
        List<Switch> Switches;
        public void _Switch()
        {
            if (Switches.Count > 0)
            {
                Switches[Index.Value].If.Invoke();
            }
            Index.Value = (int)Mathf.Repeat(Index.Value + 1, Switches.Count);
        }

        public void _ResetSwitch()
        {
            Index.Value = 0;
        }
    }
}