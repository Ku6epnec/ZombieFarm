using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SceneEditor: MonoBehaviour
{
    private List<Transform> childs = new List<Transform>();
    string jsonConteiner;

    [Serializable]
    public class TestPlayerObject
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
    }

    [Serializable]
    public class SaveContainer
    {
        public TestPlayerObject[] playerObjects;
        public Dictionary<string, Transform> PlayerDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> EnemyDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> FriendlyDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> ContructionDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> EnvironmentDictionary = new Dictionary<string, Transform>();

        /*public IPlayerObject[] playerObjects;
        public IEnemyObject[] enemyObjects;
        public IFriendlyObject[] friendlyObjects;
        public IConstructionObject[] constructionObjects;
        public IEnvironmentObject[] environmentObjects;*/
    }

    public void LoadScene()
    {
        Debug.Log("Пытаемся загрузиться");
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
        Debug.Log("Конец загрузки");
    }

    public void SaveScene()
    {
        Debug.Log("Пытаемся сохраниться");
        SaveContainer newScene = new();
        foreach(Transform child in transform)
        {
            childs.Add(child);
        }
        Debug.Log("Всего детей: " + childs.Count);
        for (int i = 0; i < childs.Count; i++)
        {
            if (childs[i].TryGetComponent<IEnvironmentObject>(out IEnvironmentObject environmentObject))
            {
                newScene.EnvironmentDictionary.Add(environmentObject.objectName, environmentObject.transform);
            }
            else if (childs[i].TryGetComponent<IPlayerObject>(out IPlayerObject playerObject))
            {
                Debug.Log("Имя объекта: " + playerObject.objectName);
                Debug.Log("Длина массива: " + newScene.playerObjects.Length);
                newScene.playerObjects = new TestPlayerObject[newScene.playerObjects.Length];
                Debug.Log("Длина массива: " + newScene.playerObjects.Length);
                newScene.playerObjects[0].name = playerObject.objectName;
                newScene.playerObjects[0].position = playerObject.transform.position;
                newScene.playerObjects[0].rotation = playerObject.transform.rotation;
                newScene.PlayerDictionary.Add(playerObject.objectName, playerObject.transform);
                //Debug.Log("Нашли объект-игрок " + playerObject.objectName);
                //Debug.Log("Его трансформ " + playerObject.transform);
                //Debug.Log("Его позиция " + playerObject.transform.position);
                //newScene.PlayerDictionary.Add(playerObject.objectName, playerObject.transform);
                //Debug.Log("Контейнер " + newScene);
                //Debug.Log("Игроки контейнера " + newScene.PlayerDictionary);
            }
            else if (childs[i].TryGetComponent<IEnemyObject>(out IEnemyObject enemyObject))
            {
                newScene.EnemyDictionary.Add(enemyObject.objectName, enemyObject.transform);
            }
            else if (childs[i].TryGetComponent<IConstructionObject>(out IConstructionObject constructionObject))
            {
                newScene.ContructionDictionary.Add(constructionObject.objectName, constructionObject.transform);
            }
            else if (childs[i].TryGetComponent<IFriendlyObject>(out IFriendlyObject friendlyObject))
            {
                newScene.FriendlyDictionary.Add(friendlyObject.objectName, friendlyObject.transform);
            }
            else Debug.Log("Этот объект не подпадает под категории, его имя: " + childs[i].name);
        }
        //string newJsonConteiner = JsonUtility.ToJson(newScene);
        Debug.Log("Что мы пытаемся записать в Json: " + JsonUtility.ToJson(newScene.playerObjects[0]));
        File.WriteAllText("Assets/jsonContainer", JsonUtility.ToJson(newScene.playerObjects[0]));
        Debug.Log("Завершаем сохранение");
    }
}
