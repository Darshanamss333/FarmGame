using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class UiTextPop
    {
        public Text _TextObject;
        Wait _wait;
        public UiTextPop(float lifeTime , string _text)
        {
            GameObject _new = new GameObject("textPop");
            _new.transform.parent = GameObject.Find("Canvas").transform;
            _TextObject = _new.AddComponent<Text>();
            _TextObject.text = _text;

            _TextObject.transform.localScale = Vector3.one;
            _TextObject.alignment = TextAnchor.MiddleCenter;
            _TextObject.horizontalOverflow = HorizontalWrapMode.Overflow;
            _TextObject.font = (Font)Resources.Load("OpenSans-ExtraBoldItalic");

            _wait = new Wait(lifeTime);
            _wait.OnTimeOutAction += delegate { GameObject.Destroy(_new); };
        }

        public void SetFollowTransform(Transform _transform , Vector3 _offset)
        {
            _wait.OnWaitAction += delegate
            {
                FadeInOut();
                _TextObject.transform.position = Camera.main.WorldToScreenPoint(_transform.position + _offset);
            };
        }


        public void SetFollowTransformWithMove(Transform _transform, Vector3 _offset , Vector3 _moveValue)
        {
            Vector3 _moveTang = Vector3.zero;
            _wait.OnWaitAction += delegate
            {
                FadeInOut();
                _moveTang += _moveValue * Time.deltaTime;
                _TextObject.transform.position = Camera.main.WorldToScreenPoint(_transform.position + _offset + _moveTang);
            };
        }

        public void SetOnlyMove(Vector3 _startPos , Vector3 _moveValue)
        {
            Vector3 _moveTang = Vector3.zero;
            _wait.OnWaitAction += delegate
            {
                FadeInOut();
                _moveTang += _moveValue * Time.deltaTime;
                _TextObject.transform.position = Camera.main.WorldToScreenPoint(_startPos + _moveTang);
            };
        }

        void FadeInOut()
        {
            if (_wait.Tang < 0.2f)
            {
                float _a = Mathf.InverseLerp(0, 0.2f, _wait.Tang);
                _TextObject.color = new Color(_TextObject.color.r, _TextObject.color.g, _TextObject.color.b, _a);
            }

            if (_wait.Tang > 0.8f)
            {
                float _a = Mathf.InverseLerp(1, 0.8f, _wait.Tang);
                _TextObject.color = new Color(_TextObject.color.r, _TextObject.color.g, _TextObject.color.b, _a);
            }
        }
    }
}
