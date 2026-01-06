using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DsoftGameKit
{
    public class CollisionDetect : MonoBehaviour
    {
        public enum CollisionTypes
        {
            Trigger,
            Collider
        }
        public enum CollisionFilters
        {
            All,
            Idamageble,
            Ihurtable,
            ByTag,
            ByGameObject
        }


        ICollisionEnter collisionEnter;
        ICollisionExit collisionExit;
        private void Awake()
        {
            collisionEnter = GetComponent<ICollisionEnter>();
            collisionExit = GetComponent<ICollisionExit>();
        }

        public CollisionTypes _CollisionType;
        public CollisionFilters _CollisionFilter;

        [HideInInspector]
        public string _TagName;
        public GameObjectReference _TargetGameObject;

        public UnityEvent _UnityEventEnter;
        public UnityEvent _UnityEvenyExit;

        [SerializeField]
        GameObjectReference HitObject;
        private void OnTriggerEnter(Collider other)
        {
            if(_CollisionType == CollisionTypes.Trigger)
            {
                switch (_CollisionFilter)
                {
                    case CollisionFilters.All:
                        Enter(other);
                        break;

                    case CollisionFilters.Idamageble:
                        IDamageble damageble = other.GetComponent<IDamageble>();
                        if (damageble != null)
                        {
                            Enter(other);
                        }
                        break;

                    case CollisionFilters.Ihurtable:
                        IHurtable hurtable = other.GetComponent<IHurtable>();
                        if (hurtable != null)
                        {
                            Enter(other);
                        }
                        break;

                    case CollisionFilters.ByTag:
                        if (other.tag == _TagName)
                        {
                            Enter(other);
                        }
                        break;

                    case CollisionFilters.ByGameObject:
                        if (other.gameObject == _TargetGameObject.Value)
                        {
                            Enter(other);
                        }
                        break;
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_CollisionType == CollisionTypes.Trigger)
            {
                switch (_CollisionFilter)
                {
                    case CollisionFilters.All:
                        Exit(other);
                        break;

                    case CollisionFilters.Idamageble:
                        IDamageble damageble = other.GetComponent<IDamageble>();
                        if (damageble != null)
                        {
                            Exit(other);
                        }
                        break;

                    case CollisionFilters.Ihurtable:
                        IHurtable hurtable = other.GetComponent<IHurtable>();
                        if (hurtable != null)
                        {
                            Exit(other);
                        }
                        break;

                    case CollisionFilters.ByTag:
                        if (other.tag == _TagName)
                        {
                            Exit(other);
                        }
                        break;

                    case CollisionFilters.ByGameObject:
                        if (other.gameObject == _TargetGameObject.Value)
                        {
                            Exit(other);
                        }
                        break;
                }
            }
        }

        void Enter(Collider other)
        {
            HitObject.Value = other.gameObject;

            if (collisionEnter != null)
            {
                collisionEnter.CollisionEnter(other.gameObject);
            }
            _UnityEventEnter.Invoke();
        }

        void Exit(Collider other)
        {
            if (HitObject.Value == other.gameObject)
            {
                HitObject.Value = null;
            }

            if (collisionExit != null)
            {
                collisionExit.CollisionExit(other.gameObject);
            }
            _UnityEvenyExit.Invoke();
        }
    }
}
