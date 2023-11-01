using System;
using UnityEngine;

[Serializable]
public class EnvironmentData : MonoBehaviour, IData
{
    public GameObject prefab;
    public Transform environmentTransform;
    public string objectName;

    public Transform ObjectTransform => environmentTransform;
    public string ObjectName => objectName;
}
