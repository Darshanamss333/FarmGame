using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SetPlayerPosition : MonoBehaviour
    {
        private void Start()
        {
            Player._Instance.transform.position = transform.position;
            GameManager._Instance._startpos = this;
        }
    }

}
