using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class LevelActions : MonoBehaviour
    {
        public void SelectNextLevel()
        {
            LevelManager._Instance.SelectedLevel += 1;
        }
    }
}
