using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DsoftGameKit
{
    public class Door2D : MonoBehaviour
    {
        [SerializeField]
        string _DoorName;
        [SerializeField]
        string _OutPutSceneName;
        [SerializeField]
        string _OutPutDoorName;
        [SerializeField]
        GameObjectReference _Player;
        [SerializeField]
        Transform _SpawnPos;
        private void Start()
        {
            if (DoorManager._Instance.DoorName == _DoorName)
            {
                _Player.Value.transform.position =_SpawnPos.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == _Player.Value)
            {
                SceneManager.LoadScene(_OutPutSceneName);
                DoorManager._Instance.DoorName = _OutPutDoorName;
            }
        }
    }
}
