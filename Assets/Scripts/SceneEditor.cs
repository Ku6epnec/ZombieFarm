using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SceneEditor: MonoBehaviour
{
    private List<Transform> childs = new List<Transform>();
    string jsonConteiner;
    public SpawnConfig spawnConfig;

    [SerializeField] private Transform PlayersTransform;
    [SerializeField] private Transform EnemiesTransform;
    [SerializeField] private Transform FriendliesTransform;
    [SerializeField] private Transform EnvironmentsTransform;
    [SerializeField] private Transform ConstructionsTransform;

    [Serializable]
    public class SceneObject
    {
        public string typeObject;
        public string nameObject;
        public Vector3 position;
        public Quaternion rotation;
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

    public List<SceneObject> MainList = new List<SceneObject>();
    public string jString;
    public int index = 0;

    public void LoadScene()
    {
        Debug.Log("Пытаемся загрузиться");
        string jsonContainer = File.ReadAllText("Assets/jsonContainer");
        string[] StringObjects = jsonContainer.Split("}{");
        Debug.Log("Строка: " + jsonContainer);
        for (int i = 0; i < StringObjects.Length; i++)
        {
            if (i == 0) StringObjects[i] = StringObjects[i] + "}";
            else if (i == StringObjects.Length - 1) StringObjects[i] = "{" + StringObjects[i];
            else StringObjects[i] = "{" + StringObjects[i] + "}";
            Debug.Log("Строки: " + StringObjects[i]);
            SceneObject obj = JsonUtility.FromJson<SceneObject>(StringObjects[i]);
            MainList.Add(obj);
        }
        Debug.Log("Новая Строка: " + MainList);

        for (int i = 0; i < MainList.Count; i++)
        {
            int j = 0;
            if (MainList[i].typeObject == "PlayerObject")
            {
                SpawnPlayerObject(i, j);
            }
            else if (MainList[i].typeObject == "EnemyObject")
            {
                SpawnEnemyObject(i, j);
            }
            else if (MainList[i].typeObject == "FriendlyObject")
            {
                SpawnFriendlyObject(i, j);
            }
            else if (MainList[i].typeObject == "ConstructionObject")
            {
                SpawnConstructionObject(i, j);
            }
            else if (MainList[i].typeObject == "EnvironmentObject")
            {
                SpawnEnvironmentObject(i, j);
            }
            //GameObject thisChild = GameObject.Find(MainList[i].nameObject);
            //Instantiate(thisChild, MainList[i].position, MainList[i].rotation);
            if (MainList[i].typeObject == "PlayerObject")
            {
                //spawnConfig.PlayerObjects[0].objectName;
            }
            Debug.Log("Тип: " + MainList[i].typeObject);
            Debug.Log("Имя: " + MainList[i].nameObject);
            Debug.Log("Позиция: " + MainList[i].position);
            Debug.Log("Ротация: " + MainList[i].rotation);
        }
        Debug.Log("Конец загрузки");
    }

    public void CleanScene()
    {
        Debug.Log("Чистим сцену");
        foreach (Transform child in transform)
        {
            if (child.gameObject.name != "Players" &&
                child.gameObject.name != "Enemies" &&
                child.gameObject.name != "Frendlies" &&
                child.gameObject.name != "Environments" &&
                child.gameObject.name != "Constrictions")
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void SpawnConstructionObject(int i, int j)
    {
        while (spawnConfig.ConstructionObjects[j].objectName != MainList[i].nameObject && spawnConfig.ConstructionObjects.Length < j)
        {
            j++;
        }
        if (spawnConfig.ConstructionObjects[j].objectName == MainList[i].nameObject)
        {
            Instantiate(spawnConfig.ConstructionObjects[j].constuctionTransform, MainList[i].position, MainList[i].rotation, ConstructionsTransform);
        }
    }

    private void SpawnFriendlyObject(int i, int j)
    {
        while (spawnConfig.FriendlyObjects[j].objectName != MainList[i].nameObject && spawnConfig.FriendlyObjects.Length < j)
        {
            j++;
        }
        if (spawnConfig.FriendlyObjects[j].objectName == MainList[i].nameObject)
        {
            Instantiate(spawnConfig.FriendlyObjects[j].friendTransform, MainList[i].position, MainList[i].rotation, FriendliesTransform);
        }
    }

    private void SpawnEnemyObject(int i, int j)
    {
        while (spawnConfig.EnemyObjects[j].objectName != MainList[i].nameObject && spawnConfig.EnemyObjects.Length < j)
        {
            j++;
        }
        if (spawnConfig.EnemyObjects[j].objectName == MainList[i].nameObject)
        {
            Instantiate(spawnConfig.EnemyObjects[j].enemyTransform, MainList[i].position, MainList[i].rotation, EnemiesTransform);
        }
    }

    private void SpawnPlayerObject(int i, int j)
    {
        while (spawnConfig.PlayerObjects[j].objectName != MainList[i].nameObject && spawnConfig.PlayerObjects.Length < j)
        {
            j++;
        }
        if (spawnConfig.PlayerObjects[j].objectName == MainList[i].nameObject)
        {
            Instantiate(spawnConfig.PlayerObjects[j].playerView, MainList[i].position, MainList[i].rotation, PlayersTransform);
        }
    }

    private void SpawnEnvironmentObject(int i, int j)
    {
        while (spawnConfig.EnvironmentObjects[j].objectName != MainList[i].nameObject && spawnConfig.EnvironmentObjects.Length < j)
        {
            j++;
        }
        if (spawnConfig.EnvironmentObjects[j].objectName == MainList[i].nameObject)
        {
            Instantiate(spawnConfig.EnvironmentObjects[j].environmentTransform, MainList[i].position, MainList[i].rotation, EnvironmentsTransform);
        }
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
        childs = null;
        MainList = null;
    }

    private void SaveFriendlyObject(IFriendlyObject friendlyObject)
    {
        int i = 0;
        while (spawnConfig.FriendlyObjects[i].objectName != friendlyObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
        }
        if (spawnConfig.FriendlyObjects[i].objectName != friendlyObject.objectName)
        {
            if (friendlyObject.TryGetComponent<FriendlyData>(out FriendlyData friendlyData))
            {
                spawnConfig.FriendlyObjects[i] = friendlyData;
            }
            else
            {
                Debug.LogError("Ошибка! У объекта " + friendlyObject.name + " отсутствует компонент FriendlyData, " +
                    "добавьте данный компонент и повторите процесс сохранения!");
            }
        }
        Debug.Log("Имя объекта: " + friendlyObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "FriendlyObject";
        thisObject.nameObject = friendlyObject.objectName;
        thisObject.position = friendlyObject.transform.position;
        thisObject.rotation = friendlyObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveConstructionObject(IConstructionObject constructionObject)
    {
        int i = 0;
        while (spawnConfig.ConstructionObjects[i].objectName != constructionObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
        }
        if (spawnConfig.ConstructionObjects[i].objectName != constructionObject.objectName)
        {
            if (constructionObject.TryGetComponent<ConstructionData>(out ConstructionData constructionData))
            {
                spawnConfig.ConstructionObjects[i] = constructionData;
            }
            else
            {
                Debug.LogError("Ошибка! У объекта " + constructionObject.name + " отсутствует компонент ConstructionData, " +
                    "добавьте данный компонент и повторите процесс сохранения!");
            }
        }
        Debug.Log("Имя объекта: " + constructionObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "ConstructionObject";
        thisObject.nameObject = constructionObject.objectName;
        thisObject.position = constructionObject.transform.position;
        thisObject.rotation = constructionObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveEnemyObject(IEnemyObject enemyObject)
    {
        int i = 0;
        while (spawnConfig.EnemyObjects[i].objectName != enemyObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
        }
        if (spawnConfig.EnemyObjects[i].objectName != enemyObject.objectName)
        {
            if (enemyObject.TryGetComponent<EnemyData>(out EnemyData enemyData))
            {
                spawnConfig.EnemyObjects[i] = enemyData;
            }
            else
            {
                Debug.LogError("Ошибка! У объекта " + enemyObject.name + " отсутствует компонент EnemyData, " +
                    "добавьте данный компонент и повторите процесс сохранения!");
            }
        }
        Debug.Log("Имя объекта: " + enemyObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnemyObject";
        thisObject.nameObject = enemyObject.objectName;
        thisObject.position = enemyObject.transform.position;
        thisObject.rotation = enemyObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SavePlayerObject(IPlayerObject playerObject)
    {
        int i = 0;
        while (spawnConfig.PlayerObjects[i].objectName != playerObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
        }
        if (spawnConfig.PlayerObjects[i].objectName != playerObject.objectName)
        {
            if (playerObject.TryGetComponent<PlayerData>(out PlayerData playerData))
            {
                spawnConfig.PlayerObjects[i] = playerData;
            }
            else
            {
                Debug.LogError("Ошибка! У объекта " + playerObject.name + " отсутствует компонент PlayerData, " +
                    "добавьте данный компонент и повторите процесс сохранения!");
            }
        }
        Debug.Log("Имя объекта: " + playerObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "PlayerObject";
        thisObject.nameObject = playerObject.objectName;
        thisObject.position = playerObject.transform.position;
        thisObject.rotation = playerObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveEnvironmentObject(IEnvironmentObject environmentObject)
    {
        int i = 0;
        while (spawnConfig.EnvironmentObjects[i].objectName != environmentObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
        }
        if (spawnConfig.EnvironmentObjects[i].objectName != environmentObject.objectName)
        {
            if (environmentObject.TryGetComponent<EnvironmentData>(out EnvironmentData environmentData))
            {
                spawnConfig.EnvironmentObjects[i] = environmentData;
            }
            else
            {
                Debug.LogError("Ошибка! У объекта " + environmentObject.name + " отсутствует компонент EnvironmentData, " +
                    "добавьте данный компонент и повторите процесс сохранения!");
            }
        }
        Debug.Log("Имя объекта: " + environmentObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnvironmentObject";
        thisObject.nameObject = environmentObject.objectName;
        thisObject.position = environmentObject.transform.position;
        thisObject.rotation = environmentObject.transform.rotation;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }
}
