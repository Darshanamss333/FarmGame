using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Stone : HarvestObjects
    {
        protected override void addresorces()
        {
            base.addresorces();
            ResourcesManager._Stone._Amount++;
            ResourcesShow.Drop(ResourcesManager._Stone, transform.position, ResourcesShow.ResourcesUiParentWorldPos);
        }

        protected override string _hitsound
        {
            get
            {
                return "StoneHitSound";
            }
        }
    }
}
