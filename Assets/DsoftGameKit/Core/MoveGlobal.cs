using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class MoveGlobal : MonoBehaviour
    {
        [SerializeField] Vector3Reference _Direction;
        private void Update()
        {
            transform.Translate(_Direction.Value * Time.deltaTime, Space.World);
        }
    }
}
