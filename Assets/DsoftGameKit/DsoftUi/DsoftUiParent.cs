using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class DsoftUiParent : MonoBehaviour
    {
        public virtual string Name
        {
            set
            {
                
            }
        }

        public virtual GameObject Body
        {
            get
            {
                return gameObject;
            }
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}
