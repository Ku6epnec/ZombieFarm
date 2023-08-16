using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SceneEditor: MonoBehaviour
{
    private List<GameObject> childs = new List<GameObject>();
    public string jsonConteiner;

    [Serializable]
    public class SceneObject
    {
        public Transform transform;
    }

    [Serializable]
    public class SaveContainer
    {
        public Dictionary<string, Transform> PlayerDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> EnemyDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> FriendlyDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> ContructionDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> EnvironmentDictionary = new Dictionary<string, Transform>();

        public IPlayerObject[] playerObjects;
        public IEnemyObject[] enemyObjects;
        public IFriendlyObject[] friendlyObjects;
        public IConstructionObject[] constructionObjects;
        public IEnvironmentObject[] environmentObjects;
    }

    public void LoadScene()
    {
        SpawnConfig spawnConfig = FindObjectOfType<SpawnConfig>();
        string jsonContainer = File.ReadAllText("Assets/jsonContainer");
        SaveContainer myScene = JsonUtility.FromJson<SaveContainer>(jsonContainer);
        foreach(var PlayerObject in myScene.PlayerDictionary)
        {
            GameObject newPlayerObject = Instantiate(GameObject.Find(PlayerObject.Key));
            newPlayerObject.transform.position = PlayerObject.Value.position;
            newPlayerObject.transform.rotation = PlayerObject.Value.rotation;
            newPlayerObject.transform.localScale = PlayerObject.Value.localScale;
        }
    }

    public void SaveScene()
    {
        SaveContainer newScene = new();
        foreach(GameObject child in transform)
        {
            childs.Add(child);
        }
        for (int i = 0; i < childs.Count; i++)
        {
            if (childs[i].TryGetComponent<IEnvironmentObject>(out IEnvironmentObject environmentObject))
            {
                newScene.EnvironmentDictionary.Add(environmentObject.objectName, environmentObject.transform);
            }
            else if (childs[i].TryGetComponent<IPlayerObject>(out IPlayerObject playerObject))
            {
                newScene.PlayerDictionary.Add(playerObject.objectName, playerObject.transform);
            }
            else if (childs[i].TryGetComponent<IEnemyObject>(out IEnemyObject enemyObject))
            {
                newScene.PlayerDictionary.Add(enemyObject.objectName, enemyObject.transform);
            }
            else if (childs[i].TryGetComponent<IConstructionObject>(out IConstructionObject constructionObject))
            {
                newScene.PlayerDictionary.Add(constructionObject.objectName, constructionObject.transform);
            }
            else if (childs[i].TryGetComponent<IFriendlyObject>(out IFriendlyObject friendlyObject))
            {
                newScene.PlayerDictionary.Add(friendlyObject.objectName, friendlyObject.transform);
            }
            else Debug.Log("Этот объект не подпадает под категории, его имя: " + childs[i].name);
        }
        string newJsonConteiner = JsonUtility.ToJson(newScene);
        File.WriteAllText("Assets/jsonContainer", newJsonConteiner);
    }
}
