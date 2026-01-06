using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{

    public class Coin2D : MonoBehaviour
    {
        [SerializeField] AudioClip _Sound;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == Player._Instance.gameObject)
            {
                hit = true;
            }
        }

        bool hit;
        private void Update()
        {
            if(hit)
            {
                Vector3 _pos = Camera.main.ScreenToWorldPoint(new Vector3(UiPlayerCoinText._Instance.transform.position.x, UiPlayerCoinText._Instance.transform.position.y, 15));
                //_pos.z = transform.position.z;
                Vector3 _dir = (_pos - transform.position).normalized;
                transform.position = transform.position += _dir * Time.deltaTime * 20;

                if (Vector3.Distance(transform.position, _pos) < 0.1f)
                {
                    GameManager._Instance._LevelCoinCount += 1;
                    SoundManager._Instance.PlaySound("coin");
                    Destroy(gameObject);
                }
            }
        }
    }
}
