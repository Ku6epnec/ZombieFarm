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
    public class SceneObject
    {
        public string typeObject;
        public string nameObject;
        public Vector3 po;
        public Quaternion ro;
    }

    [Serializable]
    public class SaveContainer
    {
        public Dictionary<string, Transform> PlayerDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> EnemyDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> FriendlyDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> ContructionDictionary = new Dictionary<string, Transform>();
        public Dictionary<string, Transform> EnvironmentDictionary = new Dictionary<string, Transform>();
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

    public List<SceneObject> MainList = new List<SceneObject>();
    public string jString;
    public int index = 0;

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
                SaveEnvironmentObject(environmentObject);
            }
            else if (childs[i].TryGetComponent<IPlayerObject>(out IPlayerObject playerObject))
            {
                SavePlayerObject(playerObject);
            }
            else if (childs[i].TryGetComponent<IEnemyObject>(out IEnemyObject enemyObject))
            {
                SaveEnemyObject(enemyObject);
            }
            else if (childs[i].TryGetComponent<IConstructionObject>(out IConstructionObject constructionObject))
            {
                SaveConstructionObject(constructionObject);
            }
            else if (childs[i].TryGetComponent<IFriendlyObject>(out IFriendlyObject friendlyObject))
            {
                SaveFriendlyObject(friendlyObject);
            }
            else Debug.Log("Этот объект не подпадает под категории, его имя: " + childs[i].name);
        }

        File.WriteAllText("Assets/jsonContainer", jString);
        Debug.Log("Завершаем сохранение");
    }

    private void SaveFriendlyObject(IFriendlyObject friendlyObject)
    {
        Debug.Log("Имя объекта: " + friendlyObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "FriendlyObject";
        thisObject.nameObject = friendlyObject.objectName;
        thisObject.po = friendlyObject.transform.position;
        thisObject.ro = friendlyObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveConstructionObject(IConstructionObject constructionObject)
    {
        Debug.Log("Имя объекта: " + constructionObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "ConstructionObject";
        thisObject.nameObject = constructionObject.objectName;
        thisObject.po = constructionObject.transform.position;
        thisObject.ro = constructionObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveEnemyObject(IEnemyObject enemyObject)
    {
        Debug.Log("Имя объекта: " + enemyObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnemyObject";
        thisObject.nameObject = enemyObject.objectName;
        thisObject.po = enemyObject.transform.position;
        thisObject.ro = enemyObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SavePlayerObject(IPlayerObject playerObject)
    {
        Debug.Log("Имя объекта: " + playerObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "PlayerObject";
        thisObject.nameObject = playerObject.objectName;
        thisObject.po = playerObject.transform.position;
        thisObject.ro = playerObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveEnvironmentObject(IEnvironmentObject environmentObject)
    {
        Debug.Log("Имя объекта: " + environmentObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnvironmentObject";
        thisObject.nameObject = environmentObject.objectName;
        thisObject.po = environmentObject.transform.position;
        thisObject.ro = environmentObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }
}
