using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DsoftGameKit
{
    public class UiPlayerCoinText : MonoBehaviour
    {
        public static UiPlayerCoinText _Instance;

        private void Awake()
        {
            _Instance = this;
        }

        [SerializeField] Text _CoinText; 
        int _deltaCoin = -1000;
        private void Update()
        {
            if (GameManager._Instance._LevelCoinCount != _deltaCoin)
            {
                _CoinText.text = GameManager._Instance._LevelCoinCount.ToString();
                _deltaCoin = GameManager._Instance._LevelCoinCount;
            }
        }
    }
}
