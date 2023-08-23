using UnityEngine;
using System;


[Serializable]
public class ConstructionData : IConstructionObject
{
    public GameObject prefab;
    public Transform constuctionTransform;
}
