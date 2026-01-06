using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class SetResolution : MonoBehaviour
    {
        public enum ScreenResolutionEnum
        {
            _1280,
            _960,
            _640
        }

        [SerializeField] ScreenResolutionEnum TargetResolution;
        [SerializeField] int _w;
        [SerializeField] int _h;
        private void Start()
        {
            _w = Screen.width;
            _h = Screen.height;
            Application.targetFrameRate = 60;

            switch (TargetResolution)
            {
                case ScreenResolutionEnum._1280:
                    if(_w > 1280)
                    {
                        float _tang = _w / 1280;
                        int newW = (int)(_w / _tang);
                        int newH = (int)(_h / _tang);
                        Screen.SetResolution(newW, newH, true);
                    }
                    break;

                case ScreenResolutionEnum._960:
                    if (_w > 960)
                    {
                        float _tang = _w / 960;
                        int newW = (int)(_w / _tang);
                        int newH = (int)(_h / _tang);
                        Screen.SetResolution(newW, newH, true);
                    }
                    break;

                case ScreenResolutionEnum._640:
                    if (_w > 640)
                    {
                        float _tang = _w / 640;
                        int newW = (int)(_w / _tang);
                        int newH = (int)(_h / _tang);
                        Screen.SetResolution(newW, newH, true);
                    }
                    break;
            }
        }
    }
}