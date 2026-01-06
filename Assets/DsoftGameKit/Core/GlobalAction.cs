using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public static class GlobalAction
    {
        //------------------------------------------------
        public static string CreateUniqueId()
        {
            string _list = "0123456789abcdefghijklmnopqrst";
            string _resault = "";
            for (int i = 0; i < 20; i++)
            {
                _resault += _list[Random.Range(0, _list.Length)];
            }

            return _resault;
        }

        public static AnimationClip GetAnimationClipFromAnimator(string _name, Animator _animator)
        {
            AnimatorClipInfo[] _clips = _animator.GetCurrentAnimatorClipInfo(0);
            for (int i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].clip.name == _name)
                {
                    return _clips[i].clip;
                }
            }

            return null;
        }


        //------------------------------------------------
        public static Vector2 GetCanvasSize()
        {
            float _value = 480f / Screen.height;
            return new Vector2(Screen.width * _value, Screen.height * _value);
        }

        //------------------------------------------------
        public static float CanvasCordinateValue()
        {
            return 480f / Screen.height;
        }

        //---------------------------------------------
        public static bool IsChild(Transform _parent, Transform _expectedChild)
        {
            foreach (Transform g in _parent.GetComponentsInChildren<Transform>())
            {
                if (g == _expectedChild)
                {
                    return true;
                }
            }

            return false;
        }

        public static GameObject GetChildByName(Transform _parent, string _expectedChildName)
        {
            foreach (Transform g in _parent.GetComponentsInChildren<Transform>())
            {
                if (g.name == _expectedChildName)
                {
                    return g.gameObject;
                }
            }

            return null;
        }


        public static Vector3 CanvasToScreenRatio
        {
            get
            {
                Vector3 ratio = new Vector3(1f / CanvasRect.sizeDelta.x * (float)Screen.width, 1f / CanvasRect.sizeDelta.y * (float)Screen.height, 0);
                return ratio;
            }
        }

        public static Vector3 ScreenToCanvasRatio
        {
            get
            {
                Vector3 ratio = new Vector3(1f / (float)Screen.width * CanvasRect.sizeDelta.x, 1f / (float)Screen.height * CanvasRect.sizeDelta.y, 0);
                return ratio;
            }
        }

        public static RectTransform CanvasRect
        {
            get
            {
                if (AppHelper._CanvasRect == null) AppHelper._CanvasRect = Canvas.GetComponent<RectTransform>();
                return AppHelper._CanvasRect;
            }
        }

        public static GameObject Canvas
        {
            get
            {
                if (AppHelper._Canvas == null) AppHelper._Canvas = GameObject.Find("Canvas");
                return AppHelper._Canvas;
            }
        }

        public static AppHelper AppHelper
        {
            get
            {
                if (AppHelper._instance)
                {
                    return AppHelper._instance;
                }
                else
                {
                    GameObject _new = new GameObject("AppHelper");
                    AppHelper _helper = _new.AddComponent<AppHelper>();
                    return _helper;
                }
            }
        }
    }
}
