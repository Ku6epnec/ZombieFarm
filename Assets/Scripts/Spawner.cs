using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static void Spawn(SpawnConfig spawnSettings)
    {
        var prefabs = spawnSettings.Objects;
        foreach(var spawnSetting in prefabs)
        {
            var pref = Instantiate(spawnSetting.prefab);
            pref.transform.position = spawnSetting.objectTransform.position;
        }
    }
}
