using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class EnemyCombo : MonoBehaviour
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

        int deadList;
        void Dead()
        {
            if (deadList == Characters.Count)
            {
                transform.parent.GetComponent<EnemyComboManager>().ActiveNextEnemyCombo();
            }
        }
    }
}
