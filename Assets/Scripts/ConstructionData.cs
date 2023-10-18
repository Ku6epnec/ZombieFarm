using UnityEngine;
using System;


[Serializable]
public class ConstructionData : MonoBehaviour, IData
{
    public GameObject prefab;
    public Transform constructionTransform;
    public string objectName;

    public Transform ObjectTransform => constructionTransform;
    public string ObjectName => objectName;
}
