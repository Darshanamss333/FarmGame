using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class UiMoveWorldObject : MonoBehaviour
    {
        [SerializeField] GameObjectReference _UiObjectPrefab;
        GameObject _uiObject;

        float _distance;
        bool _move;
        private void Update()
        {
            if(!_uiObject)
            {
                _uiObject = Instantiate(_UiObjectPrefab.Value);
                _uiObject.transform.parent = GameObject.Find("Canvas").transform;
            }

            if(Input.GetMouseButtonDown(0))
            {
                _distance = Vector3.Distance(Camera.main.transform.position, transform.position);

                if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == _uiObject)
                {
                    _move = true;
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                _move = false;
            }

            if (Input.GetMouseButton(0))
            {
                if(_move)
                {
                    Vector3 _worldpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                    transform.position = _worldpos +  (_worldpos - Camera.main.transform.position).normalized * _distance;
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
