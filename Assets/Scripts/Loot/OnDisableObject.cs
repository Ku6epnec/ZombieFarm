using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFarm.Loot
{
    public class OnDisableObject : Loot
    {
        private void OnDisable()
        {
            AddToInventory();
        }
    }
}
