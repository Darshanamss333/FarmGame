using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class WindowUi : DsoftUiParent
    {
        [SerializeField] GameObject _Body;
        public override GameObject Body
        {
            get
            {
                return _Body;
            }
        }

        [SerializeField] Text _Name;
        public override string Name 
        {
            set
            {
                _Name.text = value; 
            }
        }
    }
}
