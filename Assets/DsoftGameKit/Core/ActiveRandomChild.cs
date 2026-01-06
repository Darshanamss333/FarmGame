using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ActiveRandomChild : MonoBehaviour
    {
        private void Start()
        {
            int _randomI = Random.Range(0, transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                if(i != _randomI)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
