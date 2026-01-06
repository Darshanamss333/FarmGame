using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public enum MoveType
    {
        time,
        speed
    }
    public class ITweenMoveBy : MonoBehaviour
    {
        [SerializeField] GameObject Target;
        [SerializeField] Vector3 Amount;
        [SerializeField] bool Orienttopath;
        [SerializeField] FloatReference Looktime;

        [SerializeField] MoveType MoveType;
        [SerializeField] FloatReference Value;
        [SerializeField] FloatReference Delay;
        [SerializeField] Space Space;
        [SerializeField] iTween.EaseType EaseType;
        [SerializeField] iTween.LoopType LoopType;

        public void _MoveByObject()
        {
            iTween.MoveBy(Target, iTween.Hash("amount", Amount, "orienttopath", Orienttopath, "looktime", Looktime.Value, MoveType.ToString(), Value.Value, "space", Space, "delay", Delay.Value, "looptype", LoopType, "easetype", EaseType));
        }

        public void _StopMoveBy()
        {
            iTween.Stop(Target);
        }
    }
}
