using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class UiRotateWorldObject : MonoBehaviour
    {
        [SerializeField] GameObjectReference _UiObjectPrefab;
        GameObject _uiObject;

        Vector3 _deltaMouse;
        bool _rotate;
        private void Update()
        {
            if(!_uiObject)
            {
                _uiObject = Instantiate(_UiObjectPrefab.Value);
                _uiObject.transform.parent = GameObject.Find("Canvas").transform;
            }

            if(Input.GetMouseButtonDown(0))
            {
                _deltaMouse = Input.mousePosition;
                if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == _uiObject)
                {
                    _rotate = true;
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                _rotate = false;
            }

            if (Input.GetMouseButton(0))
            {
                if(_rotate)
                {
                    transform.RotateAround(transform.position - Camera.main.transform.position, (Input.mousePosition - _deltaMouse).x * 0.01f);
                    transform.RotateAround(Camera.main.transform.right, (Input.mousePosition - _deltaMouse).y * 0.01f);
                    _deltaMouse = Input.mousePosition;
                }
            }

            _uiObject.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }

        private void OnDisable()
        {
            if (_uiObject) Destroy(_uiObject);
        }
    }
}
