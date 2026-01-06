using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Text))]
    public class UiTextChangeEffect : MonoBehaviour
    {
        Text _TextUI;

        private void Start()
        {
            _TextUI = GetComponent<Text>();
            _deltaScale = transform.localScale;
        }

        string _deltaText;
        Vector3 _deltaScale;
        void ChangeEffect()
        {
            if(_TextUI && _deltaText != _TextUI.text)
            {
                Wait _new = new Wait(0.2f);
                _new.OnWaitAction += delegate
                {
                    if(_new.Tang < 0.5f)
                    {
                        transform.localScale = Vector3.Lerp(_deltaScale, _deltaScale * 1.5f, Mathf.InverseLerp(0f, 0.5f, _new.Tang));
                    }
                    else
                    {
                        transform.localScale = Vector3.Lerp(transform.localScale, _deltaScale, Mathf.InverseLerp(0.5f, 1f, _new.Tang));
                    }
                };

                _deltaText = _TextUI.text;
            }
        }

        private void Update()
        {
            ChangeEffect();
        }
    }
}