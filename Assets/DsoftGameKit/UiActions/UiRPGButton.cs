using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class UiRPGButton : MonoBehaviour
    {
        Image _image;
        Button _button;

        private void Start()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Clicked);
        }

        [SerializeField] FloatReference _Interval;
        [SerializeField] FloatReference _CurrentCount;
        [SerializeField] Text _IntervalText;
        [SerializeField] Text _CurrentCountText;
        [SerializeField] UnityEvent _OnClick;

        float _tang = 0;
        private void Update()
        {

            if (_tang < _Interval.Value)
            {
                _tang += Time.deltaTime;
                _button.interactable = false;
                _image.fillAmount = Mathf.InverseLerp(0, _Interval.Value, _tang);
                if (_IntervalText) _IntervalText.gameObject.SetActive(true);
            }
            else
            {
                if (_CurrentCount.Value > 0)
                {
                    if (_button.interactable == false) Effect();
                    _button.interactable = true;
                }


                if (_IntervalText) _IntervalText.gameObject.SetActive(false);
            }   

            if (_IntervalText)
            {
                _IntervalText.text = (_Interval.Value - _tang).ToString();
            }
            if (_CurrentCountText) _CurrentCountText.text = _CurrentCount.Value.ToString();
        }

        void Clicked()
        {
            _OnClick?.Invoke();
            _CurrentCount.Value -= 1;
            _tang = 0;
        }

        void Effect()
        {
            Wait _new = new Wait(0.2f);
            _new.OnWaitAction += delegate
            {
                transform.localScale = Vector3.one * _new.Tang;
            };
        }
    }
}
