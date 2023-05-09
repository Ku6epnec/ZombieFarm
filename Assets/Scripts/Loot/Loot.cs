using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Config.Links;

public class Loot : MonoBehaviour
{
    [Serializable]
    public struct ResourceAmount
    {
        public LinkToResource resource;
        public int amount;
    }

    public List<ResourceAmount> resourcesLoot;
    public List<LinkToItem> itemsLoot;

    protected bool wasUsed = false;

    public void AddToInventory()
    {
        if (wasUsed == true) return;

        foreach (ResourceAmount loot in resourcesLoot)
        {
            Root.ResourceManager.AddResource(loot.resource, loot.amount);
        }
    }
}
