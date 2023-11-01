using System;
using UnityEngine;

[Serializable]
public class EnemyData : MonoBehaviour, IData
{
    public GameObject prefab;
    public Transform enemyTransform;
    public string objectName;

    public Transform ObjectTransform => enemyTransform;
    public string ObjectName => objectName;
}