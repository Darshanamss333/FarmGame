using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class LevelFinishTrigger : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager._Instance._endpoint = this;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == Player._Instance.gameObject)
            {
                LevelManager._Instance.CompleteCurrentLevelAndUnlockNext();
                //WindowManager._Instance._CurrentWindow = WindowManager.WindowTypeEnum.LevelFinish;
            }
        }
    }
}
