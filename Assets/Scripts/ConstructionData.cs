using UnityEngine;
using System;


[Serializable]
public class ConstructionData : IConstructionObject, IData
{
    public GameObject prefab;
    public Transform constuctionTransform;

    public Transform ObjectTransform => constuctionTransform;
    public string ObjectName => objectName;
}
