using System;
using UnityEngine;

[Serializable]
public class FriendlyData : MonoBehaviour, IData
{
    public GameObject prefab;
    public Transform friendTransform;
    public string objectName;

    public Transform ObjectTransform => friendTransform;
    public string ObjectName => objectName;
}
