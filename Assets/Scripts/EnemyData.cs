using System;
using UnityEngine;

[Serializable]
public class EnemyData : IEnemyObject, IData
{
    public GameObject prefab;
    public string enemyName;
    public Transform enemyTransform;

    public Transform ObjectTransform => enemyTransform;
    public string ObjectName => objectName;
}