using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFarm.Loot
{
    public class OnDestroyLoot : Loot
    {
        private void OnDestroy()
        {
            AddToInventory();
        }
    }
}
