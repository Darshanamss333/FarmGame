using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ITweenLookUpdate : MonoBehaviour
    {
        [SerializeField] GameObject Target;
        [SerializeField] GameObjectReference LookTarget;
        [SerializeField] string Axis;
        [SerializeField] FloatReference Time;


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
            if(LookTarget.Value && Target)
            {
                if(LookOutsideTheCamera)
                {
                    iTween.LookUpdate(Target, iTween.Hash("looktarget", LookTarget.Value.transform, "axis", Axis, "time", Time.Value));
                }
                else
                {
                    if (IsVisible) iTween.LookUpdate(Target, iTween.Hash("looktarget", LookTarget.Value.transform, "axis", Axis, "time", Time.Value));
                }
            }
        }

        private void Update()
        {
            _LookUpdateObject();
        }
    }
}
