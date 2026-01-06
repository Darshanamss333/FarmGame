using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DsoftGameKit
{
    public class UiPlayerHealthText : MonoBehaviour
    {

        [SerializeField] Text _HealthText; 
        float _deltaHealth = -1000;
        private void Update()
        {
            if(Player._Instance)
            {
                if(Player._Instance.Health != _deltaHealth)
                {
                    _HealthText.text = Player._Instance.Health.ToString();
                    _deltaHealth = Player._Instance.Health;
                }
            }
        }
    }
}
