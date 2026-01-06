using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class MouseInput : MonoBehaviour
    {
        IMouseDownHold _mouseDownHold;
        IMousePosition _mousePosition;
        private void Awake()
        {
            _mouseDownHold = GetComponent<IMouseDownHold>();
            _mousePosition = GetComponent<IMousePosition>();
        }

        private void Update()
        {
            if(Application.isEditor)
            {
                if (Input.GetMouseButton(0) && _mouseDownHold != null)
                {
                    _mouseDownHold.MouseDownHold();
                }

                if (_mousePosition != null)
                {
                    _mousePosition.MousePosition(Input.mousePosition);
                }
            }
        }
    }

}
