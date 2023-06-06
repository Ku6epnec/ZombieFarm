using System;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Config.Links;

namespace ZombieFarm.Config.LinkTargets
{
    [CreateAssetMenu(fileName = nameof(LootSource), menuName = "Configs" + "/" + nameof(LootSource))]
    public class LootSource : ScriptableObject
    {
        [Serializable]
        public struct ResourceAmount
        {
            public LinkToResource resource;
            public int amount;
        }

        public List<ResourceAmount> resourcesLoot;
        public List<LinkToItem> itemsLoot;
    }
} 
