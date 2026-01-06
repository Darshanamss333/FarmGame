using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FadeCircles : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += FadeIn;
        }

        SpriteRenderer _renderer;
        bool _done;
        public void FadeIn()
        {
            if(!_done)
            {
                if (!_renderer) _renderer = GetComponent<SpriteRenderer>();
                Color _col1 = Color.black;
                _col1.a = 1;
                Color _col2 = Color.black;
                _col2.a = 0;

                Wait _new = new Wait(0.2f);
                _new.OnWaitAction += delegate
                {
                    _renderer.color = Color.Lerp(_col1, _col2, _new.Tang);
                };

                _new.OnTimeOutAction += delegate
                {
                    gameObject.SetActive(false);
                };
            }
        }
    }
}
