using UnityEngine;
using System;


[Serializable]
public class ConstructionData : MonoBehaviour, IData
{
    public GameObject prefab;
    public Transform constuctionTransform;
    public string objectName;

    public Transform ObjectTransform => constuctionTransform;
    public string ObjectName => objectName;
}
