using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "newSpawnConfig", menuName = "SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    public PlayerData[] PlayerObjects;
    public EnemyData[] EnemyObjects;
    public FriendlyData[] FriendlyObjects;
    public ConstructionData[] ConstructionObjects;
    public EnvironmentData[] EnvironmentObjects;
}

[Serializable]
public class PlayerData
{
    public ZombieFarm.Views.Player.PlayerView player;
    public Transform playerTransform;
    public Vector3 posit;
}

[Serializable]
public class EnemyData
{
    public GameObject prefab;
    public Transform enemyTransform;
}

[Serializable]
public class FriendlyData
{
    public GameObject prefab;
    public Transform friendTransform;
}

[Serializable]
public class ConstructionData
{
    public GameObject prefab;
    public Transform constuctionTransform;
}

[Serializable]
public class EnvironmentData
{
    public GameObject prefab;
    public Transform environmentTransform;
}
