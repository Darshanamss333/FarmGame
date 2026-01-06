using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public enum AxisEnum
    {
        x,y,z
    }
    [RequireComponent(typeof(SpriteRenderer))]
    public class LookUpdate : MonoBehaviour
    {
        [SerializeField] GameObject Object;
        [SerializeField] GameObjectReference Target;
        [SerializeField] FloatReference SmoothTime;


        [SerializeField]
        bool LookOutsideTheCamera;
        bool IsVisible;
        private void OnBecameVisible()
        {
            IsVisible = true;
        }
        private void OnBecameInvisible()
        {
            IsVisible = false;
        }

        public void _LookUpdateObject()
        {
            if(Target.Value && Object)
            {
                if(LookOutsideTheCamera)
                {
                    Look();
                }
                else
                {
                    if (IsVisible) Look();
                }
            }
        }

        void Look()
        {
            Vector3 _dir = Target.Value.transform.position - Object.transform.position;
            Object.transform.rotation = Quaternion.RotateTowards(Object.transform.rotation,
                Quaternion.LookRotation(_dir, transform.up), Time.time * SmoothTime.Value);
        }

        private void Update()
        {
            _LookUpdateObject();
        }
    }
}
