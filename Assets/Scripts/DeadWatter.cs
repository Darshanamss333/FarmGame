using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class DeadWatter : MonoBehaviour
    {
        float _tang;
        void Update()
        {
            if(_tang < 3)
            {
                _tang += Time.deltaTime;
            }
            else
            {
                checkpos();
                _tang = 0;
            }
        }


        Vector3 _pos;
        void checkpos()
        {
            RaycastHit _hit;
            Ray _ray = new Ray(AppManager.Farmer.transform.position + new Vector3(0, 5, 0), Vector3.down);
            if (Physics.Raycast(_ray, out _hit, 5))
            {
                if(!GlobalAction.IsChild(AppManager.Farmer.transform , _hit.collider.transform))
                {
                    _pos = _hit.point;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == AppManager.Farmer.gameObject)
            {
                WindowManager._Instance._CurrentWindow = WindowManager.WindowTypeEnum.GameOverWindow;
                AppManager.Farmer.transform.position = _pos;
                Wait _new = new Wait(1);
                _new.OnTimeOutAction += delegate
                {
                    WindowManager._Instance._CurrentWindow = WindowManager.WindowTypeEnum.GameWindow;
                };
            }
        }
    }
}
