using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class CallSmallCameraShake : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += Shake;
        }
        public void Shake()
        {
            CameraShake._instance.SmallShake();
        }
    }
}