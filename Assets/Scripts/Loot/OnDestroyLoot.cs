using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyLoot : Loot
{
    private void OnDestroy()
    {
        AddToInventory();
        wasUsed = true;
    }
}
