using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class IfAllDead : MonoBehaviour
    {
        [SerializeField] List<BaseCharacter> Characters;
        private void Start()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].OnDieAction += delegate
                {
                    deadList += 1;
                    Dead();
                };
            }
        }

        [SerializeField] UnityEvent OnDeadListComplete;
        [SerializeField] int deadList;
        void Dead()
        {
            if (deadList == Characters.Count)
            {
                OnDeadListComplete?.Invoke();
            }
        }
    }
}
