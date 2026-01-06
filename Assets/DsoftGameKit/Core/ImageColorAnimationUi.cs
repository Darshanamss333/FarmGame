using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class ImageColorAnimationUi : MonoBehaviour
    {
        [SerializeField] Image _Image;
        [SerializeField] float _BlendSpeed = 1;
        [SerializeField] List<Color> _List = new List<Color>();


        private void Start()
        {
            change();
        }

        Color _curColor;
        Color _deltaCol;
        int _curindex;
        void change()
        {
            tang = 0;
            _curindex += 1;
            _deltaCol = _List[(int)Mathf.Repeat(_curindex, _List.Count)];
            _curColor = _List[(int)Mathf.Repeat(_curindex + 1, _List.Count)];
        }

        float tang = 0;
        private void Update()
        {
            tang += Time.deltaTime * _BlendSpeed;

            if (tang > 1)
            {
                change();
            }

            _Image.color = Color.Lerp(_deltaCol, _curColor, tang);
        }
    }
}