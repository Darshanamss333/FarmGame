using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class GoldMine : AutomatedObjects
    {
        protected override ResourcesManager.ResType returntype => ResourcesManager._Coin;
    }
}
