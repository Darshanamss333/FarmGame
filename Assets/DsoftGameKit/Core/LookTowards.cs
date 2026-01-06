using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class LookTowards : MonoBehaviour
    {
        [SerializeField] GameObjectReference Object;
        [SerializeField] GameObjectReference Target;
        [SerializeField] FloatReference Speed;

        [SerializeField] UnityEvent OnFinish;
        bool complete;
        private void OnEnable()
        {
            complete = false;
        }

        private void Update()
        {
            if(!complete)
            {
                Vector3 _dir = Target.Value.transform.position - Object.Value.transform.position;
                _dir.y = 0;
                Object.Value.transform.rotation = Quaternion.RotateTowards(Object.Value.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), Time.time * Speed.Value);

                if (Vector3.Angle(Object.Value.transform.forward , _dir) < 0.1f)
                {
                    OnFinish.Invoke();
                    complete = true;
                }
            }
        }
    }
}