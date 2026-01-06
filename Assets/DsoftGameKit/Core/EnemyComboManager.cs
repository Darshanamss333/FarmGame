using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class EnemyComboManager : MonoBehaviour
    {
        [SerializeField] FloatReference Index;

        private void OnEnable()
        {
            ActiveCurrentCombo();
        }

        [SerializeField] UnityEvent ComboComplete;
        public void ActiveNextEnemyCombo()
        {
            if(Index.Value + 1 == transform.childCount)
            {
                ComboComplete?.Invoke();
            }
            else
            {
                Wait _new = new Wait(1);
                _new.OnTimeOutAction += delegate
                {
                    Index.Value += 1;
                    ActiveCurrentCombo();
                };
            }
        }

        void ActiveCurrentCombo()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == Index.Value)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
