using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class WorldToScreenPosition : MonoBehaviour
    {
        [SerializeField] GameObjectReference UiObject;
        [SerializeField] GameObjectReference TargetWorldObject;


        Camera camera;
        private void Update()
        {
            if(camera)
            {
                if(TargetWorldObject.Value) transform.position = camera.WorldToScreenPoint(TargetWorldObject.Value.transform.position);
            }
            else
            {
                camera = Camera.main.GetComponent<Camera>();
            }

            if(TargetWorldObject.Value && TargetWorldObject.Value.active)
            {
                UiObject.Value.SetActive(true);
            }
            else
            {
                UiObject.Value.SetActive(false);
            }
        }
    }
}