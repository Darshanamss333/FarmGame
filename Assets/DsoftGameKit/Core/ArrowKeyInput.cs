using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class ArrowKeyInput : MonoBehaviour
    {
        IArrowKeyInput _arrowKeyInput;
        private void Awake()
        {
            _arrowKeyInput = GetComponent<IArrowKeyInput>();
        }

        private void Update()
        {
            if (_arrowKeyInput != null)
            {
                _arrowKeyInput.ArrowKeyInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }
    }
}
