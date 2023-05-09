using System;
using UnityEngine;
using ZombieFarm.Config.Links;

namespace ZombieFarm.Config.LinkTargets
{
    [CreateAssetMenu(fileName = nameof(Item), menuName = "Configs" + "/" + nameof(Item))]
    public class Item : ScriptableObject
    {
        [Serializable]
        public struct Data
        {
            [Serializable]
            public struct Currency
            {
            }

            [Serializable]
            public struct Score
            {
            }

            [Serializable]
            public struct Tool
            {
                public Operation[] operations;
                public long maxDurability;
            }

            [Serializable]
            public struct Material
            {
            }

            [Serializable]
            public struct CraftRecipe
            {
                public int playerLevelToUnlock;
                public int input;
                public int output;
                public long duration;
            }

            [Serializable]
            public struct Operation
            {
                public int type;
            }

            public int type;
            public float worth;
            public Currency currency;
            public Score score;
            public Tool tool;
            public Material material;
            public CraftRecipe craftRecipe;
        }

        public Sprite sprite;
        public bool isSearchable;
        public GameObject viewPrefab;
        public GameObject inHandPrefab;
        public Data data;
    }
}
