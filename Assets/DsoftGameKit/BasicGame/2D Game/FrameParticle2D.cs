using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class FrameParticle2D : CommenEvents
    {
        public override void AfterAwake()
        {
            base.AfterAwake();
            _OnAllAction += Particel;
        }


        [SerializeField] int _Count = 1;
        [SerializeField] float _Fps = 12;
        [SerializeField] float _SizeMuliply = 1;
        [SerializeField] string _SortingLayer = "Default";
        [SerializeField] Color _Color = Color.white;
        [SerializeField] List<Sprite> _Sprites;
        public void Particel()
        {
            Wait _new = new Wait(1f/ _Fps * _Sprites.Count * _Count);
            _new.Object.name = "FrameParticle2D";
            _new.Object.transform.position = transform.position;
            _new.Object.transform.localScale = Vector3.one * _SizeMuliply;
            SpriteRenderer _renderr = _new.Object.AddComponent<SpriteRenderer>();
            _renderr.sortingLayerName = _SortingLayer;
            _renderr.color = _Color;
            SpriteAnimation2D _anim = _new.Object.AddComponent<SpriteAnimation2D>();
            _anim._Renderer = _renderr;
            _anim._Sprites = _Sprites;
            _anim._Fps = _Fps;
        }
    }
}
