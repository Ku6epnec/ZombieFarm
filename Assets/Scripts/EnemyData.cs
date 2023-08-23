using System;
using UnityEngine;

[Serializable]
public class EnemyData : IEnemyObject
{
    public GameObject prefab;
    public string enemyName;
    public Transform enemyTransform;
}