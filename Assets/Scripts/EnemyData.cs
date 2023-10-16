using System;
using UnityEngine;

[Serializable]
public class EnemyData : MonoBehaviour, IData
{
    public GameObject prefab;
    public string enemyName;
    public Transform enemyTransform;
    public string objectName;

    public Transform ObjectTransform => enemyTransform;
    public string ObjectName => objectName;
}