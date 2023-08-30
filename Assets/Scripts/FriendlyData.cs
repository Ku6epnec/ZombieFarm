using System;
using UnityEngine;


[Serializable]
public class FriendlyData : IFriendlyObject, IData
{
    public GameObject prefab;
    public Transform friendTransform;

    public Transform ObjectTransform => friendTransform;
    public string ObjectName => objectName;
}
