using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SpriteState : MonoBehaviour
    {
        [System.Serializable]
        public class Logic
        {
            public int _Health;
            public Sprite _Sprite;
        }

        public SpriteRenderer _Renderer;
        public List<Logic> Data;

        private void Start()
        {
            GetComponent<BaseCharacter>().OnDamageAction += RefreshSprite;
        }

        public void RefreshSprite()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                if(GetComponent<BaseCharacter>().Health <= Data[i]._Health)
                {
                    _Renderer.sprite = Data[i]._Sprite;
                }
            }
        }
    }
}
