using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Room : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Rooms;
        GameObject _room;
        private void Start()
        {
            _room = Instantiate(_Rooms.Value, transform);
            _room.transform.localPosition = Vector3.zero;
        }


        [SerializeField] RoomTrigger _RoomTrigger;
        private void Update()
        {
            if(_RoomTrigger._Output.Value == _RoomTrigger.gameObject)
            {
                _room.transform.Find("FrontGround")?.gameObject.SetActive(true);
                _room.transform.Find("Enemy")?.gameObject.SetActive(true);

            }
            else
            {
                _room.transform.Find("FrontGround")?.gameObject.SetActive(false);
                _room.transform.Find("Enemy")?.gameObject.SetActive(false);
            }

        }
    }

}
