using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class UiTextPanelPop : MonoBehaviour
    {
        [SerializeField] Sprite _PanelSprite;
        [SerializeField] Color _PanelColor = Color.white;
        [SerializeField] Vector2 _PanelSize;
        [SerializeField] Vector3 _Offset;

        [SerializeField] StringReference _Text;
        [SerializeField] Font _Font;
        [SerializeField] int _FontSize = 14;
        [SerializeField] Color _FontColor = Color.black;

        GameObject _panelObject;
        public void ShowPanel()
        {
            if(!_panelObject)
            {
                _panelObject = new GameObject();
                _panelObject.transform.parent = GameObject.Find("Canvas").transform;
                Image _image = _panelObject.AddComponent<Image>();
                _image.sprite = _PanelSprite;
                _image.color = _PanelColor;
                _image.GetComponent<RectTransform>().sizeDelta = _PanelSize;
                _image.type = Image.Type.Sliced;


                GameObject _textObject = new GameObject();
                _textObject.transform.parent = _panelObject.transform;
                _textObject.transform.localScale = Vector3.one;
                Text _text = _textObject.AddComponent<Text>();
                _text.text = _Text.Value;
                _text.fontSize = _FontSize;
                _text.GetComponent<RectTransform>().sizeDelta = _PanelSize;
                _text.alignment = TextAnchor.MiddleCenter;
                _text.font = _Font;
                _text.color = _FontColor;


                Wait _newWait = new Wait(0.05f);
                _newWait.OnWaitAction += delegate
                {
                    if (_panelObject) _panelObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _newWait.Tang);
                };
            }

        }

        public void HidePanel()
        {
            if(_panelObject)
            {
                Wait _newWait = new Wait(0.05f);
                _newWait.OnWaitAction += delegate
                {
                    if (_panelObject) _panelObject.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _newWait.Tang);
                };

                _newWait.OnTimeOutAction += delegate
                {
                    if (_panelObject) GameObject.Destroy(_panelObject);
                };
            }
        }

        private void Update()
        {
            if(_panelObject) _panelObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + _Offset);
        }
    }
}
