using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public enum pathTypes
    {
        position,
        localPath,
        globlePath,
    }
    public class ITweenMoveTo : MonoBehaviour
    {
        [HideInInspector] public GameObject Target;
        [HideInInspector] public pathTypes PathType;
        [HideInInspector] public Vector3 Position;
        [HideInInspector] public Vector3[] LocalPath;
        [HideInInspector] public Path GlobalPath;

        [HideInInspector] public bool ClosePath;
        [HideInInspector] public bool ShowWaypoints;

        [SerializeField] bool MoveToPath;
        [SerializeField] bool Orienttopath;
        [SerializeField] FloatReference Looktime;
        [SerializeField] MoveType MoveType;
        [SerializeField] FloatReference Value;
        [SerializeField] FloatReference Delay;
        [SerializeField] iTween.LoopType LoopType;
        [SerializeField] iTween.EaseType EaseType;

        public void _MoveToObject()
        {
            switch (PathType)
            {
                case pathTypes.position:
                    iTween.MoveTo(Target, iTween.Hash("position", Position, MoveType.ToString(), Value.Value, "movetopath", MoveToPath, "orienttopath", Orienttopath, "looktime", Looktime.Value, "delay", Delay.Value, "looptype", LoopType, "easetype", EaseType, "oncomplete", "_onComplete", "oncompletetarget", gameObject));
                    break;

                case pathTypes.localPath:
                    iTween.MoveTo(Target, iTween.Hash("path", LocalPath, MoveType.ToString(), Value.Value, "movetopath", MoveToPath, "orienttopath", Orienttopath, "looktime", Looktime.Value, "delay", Delay.Value, "looptype", LoopType, "easetype", EaseType, "oncomplete", "_onComplete", "oncompletetarget", gameObject));
                    break;

                case pathTypes.globlePath:
                    iTween.MoveTo(Target, iTween.Hash("path", GlobalPath.Points, MoveType.ToString(), Value.Value, "movetopath", MoveToPath, "orienttopath", Orienttopath, "looktime", Looktime.Value, "delay", Delay.Value, "looptype", LoopType, "easetype", EaseType, "oncomplete", "_onComplete", "oncompletetarget", gameObject));
                    break;
            }


        }

        public UnityEvent OnComplete;
        void _onComplete()
        {
            OnComplete.Invoke();
        }

        public void _StopToMove()
        {
            iTween.Stop(Target);
        }

        public void _StoreCurrentPosition()
        {
            Position = transform.position;
        }

        private void Start()
        {
            _MoveToObject();
        }

    }
}
