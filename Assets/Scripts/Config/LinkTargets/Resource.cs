using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFarm.Config.LinkTargets
{
    [CreateAssetMenu(fileName = nameof(Resource), menuName = "Configs" + "/" + nameof(Resource))]
    public class Resource : ScriptableObject
    {
        public string displayName;
        public float worth;
        public Sprite sprite;
    }
}
