using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class OpenUrl : MonoBehaviour
    {
        public void _OpenUrl(string _Url)
        {
            Application.OpenURL(_Url);
        }
    }
}
