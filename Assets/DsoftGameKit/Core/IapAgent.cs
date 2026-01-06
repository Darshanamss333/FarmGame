/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class IapAgent : MonoBehaviour
    {
        public string _Product;
        [SerializeField] UnityEvent _Oncomplete;
        public void OnComplete()
        {
            _Oncomplete?.Invoke();
        }

        Button _button;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(delegate
            {
                IapManager._Instance.Buy(_Product);
            });
        }
    }
}
*/