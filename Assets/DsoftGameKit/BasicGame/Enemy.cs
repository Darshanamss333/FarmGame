using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Enemy : BaseCharacter, IDamageble
    {
        void IDamageble.Damage(float _value)
        {
            base.Damage(_value);
        }
    }

}
