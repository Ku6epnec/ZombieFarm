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

/*[Serializable]
public class PlayerData: IPlayerObject
{
    public ZombieFarm.Views.Player.PlayerView playerView;
    public ZombieFarm.Views.Player.PlayerMover playerMover;
    public Transform playerTransform;
    public Vector3 posit;
}*/

[Serializable]
public class EnemyData: IEnemyObject
{
    public GameObject prefab;
    public string enemyName;
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
