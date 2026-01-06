using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class WindowActions : MonoBehaviour
    {
        [SerializeField] WindowManager.WindowTypeEnum _Type;
        public void SetWindow()
        {
            WindowManager._Instance._CurrentWindow = _Type;
        }
    }

}
