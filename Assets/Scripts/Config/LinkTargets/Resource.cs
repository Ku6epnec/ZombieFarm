using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Config.Links;

namespace ZombieFarm.Config.LinkTargets
{
    [CreateAssetMenu(fileName = nameof(Resource), menuName = "Configs" + "/" + nameof(Resource))]
    public class Resource : ScriptableObject
    {
        [Serializable]
        public struct WorthResources
        {
            public LinkToResource linkToOtherResource;
            public int thisWorth;
            public int otherWorth;
        }

        public string displayName;
        public Sprite sprite;
        public List<WorthResources> worthResources;
    }
}
