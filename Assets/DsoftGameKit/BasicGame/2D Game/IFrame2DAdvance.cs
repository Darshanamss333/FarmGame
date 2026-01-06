using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class IFrame2DAdvance : CommenEvents
    {
        [SerializeField] List<SpriteRenderer> _RendererList;

        private void Start()
        {
            _OnAllAction += IFrame;
        }

        public void IFrame()
        {
            float _deltaTang = 0;
            Wait _new = new Wait(2);
            _new.OnWaitAction += delegate
            {
                if (_deltaTang <= _new.CurrentTime - 0.05f)
                {
                    active(!_RendererList[0].enabled);
                    _deltaTang = _new.CurrentTime;
                }
            };

            _new.OnTimeOutAction += () => active(true);
        }

        void active(bool _value)
        {
            for (int i = 0; i < _RendererList.Count; i++)
            {
                _RendererList[i].enabled = _value;
            }
        }
    }
}
