using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class UiButtonPop : MonoBehaviour
    {
        [SerializeField] Sprite _ButtonSprite;
        [SerializeField] Color _ButtonColor = Color.white;
        [SerializeField] Vector2 _ButtonSize;
        [SerializeField] Vector3 _Offset;

        [SerializeField] StringReference _Text;
        [SerializeField] Font _Font;
        [SerializeField] int _FontSize = 14;
        [SerializeField] Color _FontColor = Color.black;
        [SerializeField] UnityEvent _OnClickEvent;

        GameObject _ButtonObject;
        public void ShowPanel()
        {
            if(!_ButtonObject)
            {
                _ButtonObject = new GameObject();

                _ButtonObject.transform.parent = GameObject.Find("Canvas").transform.Find("PopupParent");
                Image _image = _ButtonObject.AddComponent<Image>();
                _image.sprite = _ButtonSprite;
                _image.color = _ButtonColor;
                _image.GetComponent<RectTransform>().sizeDelta = _ButtonSize;
                _image.type = Image.Type.Sliced;
                _ButtonObject.AddComponent<Shadow>();

                Button _button = _ButtonObject.AddComponent<Button>();
                _button.onClick.AddListener(onclick);
               

                GameObject _textObject = new GameObject();
                _textObject.transform.parent = _ButtonObject.transform;
                _textObject.transform.localScale = Vector3.one;
                Text _text = _textObject.AddComponent<Text>();
                _text.text = _Text.Value;
                _text.fontSize = _FontSize;
                _text.GetComponent<RectTransform>().sizeDelta = _ButtonSize;
                _text.alignment = TextAnchor.MiddleCenter;
                _text.font = _Font;
                _text.color = _FontColor;


                Wait _newWait = new Wait(0.05f);
                _newWait.OnWaitAction += delegate
                {
                    if (_ButtonObject) _ButtonObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _newWait.Tang);
                };
            }

        }

        [SerializeField] bool ShowOnEnable;
        private void OnEnable()
        {
            if (ShowOnEnable) ShowPanel();
        }

        [SerializeField] bool HideOnDisable;
        private void OnDisable()
        {
            if (HideOnDisable) HidePanel();
        }

        public void HidePanel()
        {
            if(_ButtonObject)
            {
                Wait _newWait = new Wait(0.05f);
                _newWait.OnWaitAction += delegate
                {
                    if (_ButtonObject) _ButtonObject.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _newWait.Tang);
                };

                _newWait.OnTimeOutAction += delegate
                {
                    if (_ButtonObject) GameObject.Destroy(_ButtonObject);
                };
            }
        }

        private void Update()
        {
            if(_ButtonObject) _ButtonObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + _Offset);
        }

        void onclick()
        {
            _OnClickEvent?.Invoke();
        }
    }
}
