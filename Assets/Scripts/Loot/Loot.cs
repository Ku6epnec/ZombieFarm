using UnityEngine;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;

namespace ZombieFarm.Loot
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private LinkToLootSource link;

        public void AddToInventory()
        {
            LootSource lootSource = Root.ConfigManager.GetByLink<LootSource>(link);

            foreach (var loot in lootSource.resourcesLoot)
            {
                Root.ResourceManager.AddResource(loot.resource, loot.amount);
            }
        }
    }
}