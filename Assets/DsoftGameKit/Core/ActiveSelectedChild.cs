using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveSelectedChild : MonoBehaviour
    {
        [SerializeField] IntReference Index;
        float _deltaIndx;
        private void Update()
        {
            if(_deltaIndx != Index.Value)
            {
                UpdateActiveChild();
                _deltaIndx = Index.Value;
            }
        }

        private void OnEnable()
        {
            UpdateActiveChild();
        }

        void UpdateActiveChild()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(i == Index.Value)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        public void AddIndex(int value)
        {
            Index.Value += value;
        }
    }
}
