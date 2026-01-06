using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class UiProgressBar : MonoBehaviour
    {
        [SerializeField] FloatReference _CurrentValue;
        [SerializeField] FloatReference _MaxValue;
        [SerializeField] BoolReference _AutoRefill;
        [SerializeField] FloatReference _RefilSpeed;
        [SerializeField] Image _UiBar;

        private void Update()
        {
            _UiBar.fillAmount = 1f / _MaxValue.Value * _CurrentValue.Value;

            if (_AutoRefill.Value)
            {
                _CurrentValue.Value = Mathf.Clamp(_CurrentValue.Value + _RefilSpeed.Value * Time.deltaTime, 0, _MaxValue.Value);
            }
        }
    }

}
