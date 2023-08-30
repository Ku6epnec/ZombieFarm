using System;
using UnityEngine;

[Serializable]
public class EnvironmentData : IEnvironmentObject, IData
{
    public GameObject prefab;
    public Transform environmentTransform;

    public Transform ObjectTransform => environmentTransform;
    public string ObjectName => objectName;
}
