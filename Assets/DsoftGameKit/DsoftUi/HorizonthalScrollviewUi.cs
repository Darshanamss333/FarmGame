using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class HorizonthalScrollviewUi : DsoftUiParent
    {
        [SerializeField] GameObject _Body;
        public override GameObject Body
        {
            get
            {
                return _Body;
            }
        }
    }
}
