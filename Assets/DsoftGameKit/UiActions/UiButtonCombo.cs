using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class UiButtonCombo : MonoBehaviour
    {
        Button BT;
        private void Start()
        {
            BT = GetComponent<Button>();
            BT.onClick.AddListener(delegate { Onclick(); });
        }

        //OnEnable--------------------------------------
        void OnEnable()
        {
            OnEnableAnimation();
        }

        //OnClick---------------------------------------
        void Onclick()
        {
            ClickSound();
            OnClickAnimation();
        }

        //ClickSound-------------------------------------
        AudioSource ClickAudio;
        void ClickSound()
        {
            if(!ClickAudio)
            {
                GameObject _new = new GameObject();
                _new.name = gameObject.name + " Button Sound";
                ClickAudio = _new.AddComponent<AudioSource>();
                ClickAudio.clip = Resources.Load("Sounds/ButtonClick") as AudioClip;
            }

            ClickAudio.Play();
        }


        //OnEnableAnimation------------------------------
        Vector3 OriginalSize;
        void OnEnableAnimation()
        {
            OriginalSize = transform.localScale;
            transform.localScale = Vector3.zero;
            iTween.ScaleTo(gameObject, OriginalSize, 0.1f);
        }

        //OnClickAnimation------------------------------
        void OnClickAnimation()
        {
            iTween.PunchScale(gameObject, iTween.Hash("amount" , Vector3.one * 0.5f , "time" , 0.1f , "oncomplete", "AfterClickAnimation"));
            //iTween.ScaleTo(gameObject, iTween.Hash("scale" , Vector3.zero , "time" , 0.1f , "oncomplete", "AfterClickAnimation"));
        }

        //AfterClickAnimation;--------------------------
        [SerializeField]
        UnityEvent OnAfterClickAnimation;
        void AfterClickAnimation()
        {
            OnAfterClickAnimation?.Invoke();
        }
    }
}
