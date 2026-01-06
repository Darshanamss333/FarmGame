using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] GameObjectReference _Player;
        public GameObjectReference _Output;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform.gameObject == _Player.Value)
            {
                _Output.Value = gameObject;
            }
        }
    }
}
