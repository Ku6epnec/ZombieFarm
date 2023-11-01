using UnityEngine;

[CreateAssetMenu(fileName = "newSpawnConfig", menuName = "SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    public PlayerData[] PlayerObjects;
    public EnemyData[] EnemyObjects;
    public FriendlyData[] FriendlyObjects;
    public ConstructionData[] ConstructionObjects;
    public EnvironmentData[] EnvironmentObjects;
}

