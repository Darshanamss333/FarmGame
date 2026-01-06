using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class Tree : HarvestObjects
    {
        protected override void addresorces()
        {
            base.addresorces();
            ResourcesManager._Wood._Amount++;
            ResourcesShow.Drop(ResourcesManager._Wood , transform.position, ResourcesShow.ResourcesUiParentWorldPos);
        }

        protected override string _hitsound
        {
            get
            {
                return "TreeHitSound";
            }
        }
    }
}
