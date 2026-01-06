using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class KeyEvents : MonoBehaviour
    {
        [SerializeField] KeyCode Key;
        [SerializeField] UnityEvent OnKeyDown;
        [SerializeField] UnityEvent OnKeyUp;
        private void Update()
        {
            if(Input.GetKeyDown(Key))
            {
                OnKeyDown?.Invoke();
            }

            if(Input.GetKeyUp(Key))
            {
                OnKeyUp?.Invoke();
            }
        }
    }
}
