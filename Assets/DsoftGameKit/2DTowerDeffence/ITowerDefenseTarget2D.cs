using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    interface ITowerDefenseTarget2D 
    {
        GameObject GetTarget();

        void SetTarget(GameObject _value);
    }
}
