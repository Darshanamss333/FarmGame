using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveSelectedChildFromList : MonoBehaviour
    {
        [SerializeField] List<GameObject> _List;
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
            for (int i = 0; i < _List.Count; i++)
            {
                if(i == Index.Value)
                {
                    _List[i].SetActive(true);
                }
                else
                {
                    _List[i].SetActive(false);
                }
            }
        }

        public void AddIndex(int value)
        {
            Index.Value += value;
        }
    }
}
