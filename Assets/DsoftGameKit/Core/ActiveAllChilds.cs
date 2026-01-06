using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveAllChilds : CommenEvents
    {
        private void Start()
        {
            _OnAllAction += ActiveChilds;
        }

        public void ActiveChilds()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
