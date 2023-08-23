using System;
using UnityEngine;

[Serializable]
public class EnvironmentData : IEnvironmentObject
{
    public GameObject prefab;
    public Transform environmentTransform;
}
