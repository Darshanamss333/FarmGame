using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager _Instance
        {
            get
            {
                if (SoundManager.LocalInstance == null)
                {
                    GameObject _new = new GameObject();
                    _new.name = "SoundManager";
                    SoundManager _Instance = _new.AddComponent<SoundManager>();
                    LocalInstance = _Instance;
                    DontDestroyOnLoad(LocalInstance.gameObject);
                    return _Instance;
                }
                else
                {
                    return SoundManager.LocalInstance;
                }

            }
        }

        public static SoundManager LocalInstance;
        /*
        [SerializeField] AudioSource _GameOver;
        [SerializeField] AudioSource _Levels;
        [SerializeField] AudioSource _LevelsComplete;

        WindowManager.WindowTypeEnum _deltawindow = WindowManager.WindowTypeEnum.None;
        private void Update()
        {

            if (WindowManager._Instance._CurrentWindow != _deltawindow)
            {

                if (WindowManager._Instance._CurrentWindow == WindowManager.WindowTypeEnum.GameOverWindow)
                {
                    _GameOver.Play();
                    _Levels.Stop();
                }

                if (WindowManager._Instance._CurrentWindow == WindowManager.WindowTypeEnum.GameWindow)
                {
                    _GameOver.Stop();
                    _Levels.Play();
                }

                if (WindowManager._Instance._CurrentWindow == WindowManager.WindowTypeEnum.LevelFinish)
                {
                    _LevelsComplete.Play();
                    _Levels.Stop();
                }

                _deltawindow = WindowManager._Instance._CurrentWindow;
            }
        }
        */

        public void PlaySound(string _name)
        {
            AudioClip _clip = Resources.Load(_name) as AudioClip;
            if(_clip)
            {
                Wait _new = new Wait(_clip.length);
                AudioSource _source = _new.Object.AddComponent<AudioSource>();
                _source.clip = _clip;
                _source.Play();
            }

        }
    }
}
