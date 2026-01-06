using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class MouseClickOver2DObjectEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent _OnClickEvent;
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 100);

                if(_hit.collider != null && _hit.collider.gameObject == gameObject)
                {
                    _OnClickEvent?.Invoke();
                }
            }
        }
    }
}
