using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class MoveTowards : MonoBehaviour
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
                Object.Value.transform.position = Vector3.MoveTowards(Object.Value.transform.position, Target.Value.transform.position, Time.deltaTime * Speed.Value);

                if(Vector3.Distance(Object.Value.transform.position , Target.Value.transform.position) < 0.1f)
                {
                    OnFinish.Invoke();
                    complete = true;
                }
            }
        }
    }
}