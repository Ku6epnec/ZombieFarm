using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "newSpawnConfig", menuName = "SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    public ObjectData[] Objects;
}

[Serializable]
public class ObjectData
{
    public GameObject prefab;
    public Transform objectTransform;
}
