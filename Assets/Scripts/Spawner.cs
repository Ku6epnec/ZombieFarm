using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static void Spawn(SpawnConfig spawnSettings, Transform transform)
    {
        var players = spawnSettings.PlayerObjects;
        foreach(var spawnSetting in players)
        {
            var pref = Instantiate(spawnSetting.player, transform);
            pref.transform.position = spawnSetting.posit;
        }
        var enemies = spawnSettings.EnemyObjects;
        foreach (var spawnSetting in enemies)
        {
            var pref = Instantiate(spawnSetting.prefab, transform);
            pref.transform.position = spawnSetting.enemyTransform.position;
        }
        var frends = spawnSettings.FriendlyObjects;
        foreach (var spawnSetting in frends)
        {
            var pref = Instantiate(spawnSetting.prefab, transform);
            pref.transform.position = spawnSetting.friendTransform.position;
        }
        var contructions = spawnSettings.ConstructionObjects;
        foreach (var spawnSetting in contructions)
        {
            var pref = Instantiate(spawnSetting.prefab, transform);
            pref.transform.position = spawnSetting.constuctionTransform.position;
        }
        var environments = spawnSettings.EnvironmentObjects;
        foreach (var spawnSetting in environments)
        {
            var pref = Instantiate(spawnSetting.prefab, transform);
            pref.transform.position = spawnSetting.environmentTransform.position;
        }
    }
}
