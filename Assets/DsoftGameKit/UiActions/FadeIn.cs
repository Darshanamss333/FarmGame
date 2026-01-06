using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Image))]
    public class FadeIn : MonoBehaviour
    {

        Image _image;
        private void Start()
        {
            _image = GetComponent<Image>();
            Wait _new = new Wait(1);
            _new.OnWaitAction += delegate
            {
                Color _col = Color.black;
                _col.a = 1f - _new.Tang;
                _image.color = _col;
            };
        }
    }
}
