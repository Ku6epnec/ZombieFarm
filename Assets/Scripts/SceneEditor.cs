using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneEditor: MonoBehaviour
{
    private struct SpawnParameters
    {
        public IData[] dataArray;
        public Transform parentTransform;
    }

    private List<Transform> childs = new List<Transform>();
    [SerializeField] private string jsonFile;
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
        public Vector3 scale;
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
        string jsonContainer = File.ReadAllText("Assets/" + jsonFile);
        string[] StringObjects = jsonContainer.Split("}{");
        Debug.Log("Строка: " + jsonContainer);
        for (int NumberOfObjectInJson = 0; NumberOfObjectInJson < StringObjects.Length; NumberOfObjectInJson++)
        {
            if (NumberOfObjectInJson == 0) StringObjects[NumberOfObjectInJson] = StringObjects[NumberOfObjectInJson] + "}";
            else if (NumberOfObjectInJson == StringObjects.Length - 1) StringObjects[NumberOfObjectInJson] = "{" + StringObjects[NumberOfObjectInJson];
            else StringObjects[NumberOfObjectInJson] = "{" + StringObjects[NumberOfObjectInJson] + "}";
            Debug.Log("Строки: " + StringObjects[NumberOfObjectInJson]);
            SceneObject obj = JsonUtility.FromJson<SceneObject>(StringObjects[NumberOfObjectInJson]);
            MainList.Add(obj);
        }
        Debug.Log("Новая Строка: " + MainList);

        for (int NumberOfElementInMainList = 0; NumberOfElementInMainList < MainList.Count; NumberOfElementInMainList++)
        {
            int NumberOfElementInDataArray = 0;

            SpawnObject(NumberOfElementInMainList, NumberOfElementInDataArray, GetSpawnParameters(MainList[NumberOfElementInMainList].typeObject));

            Debug.Log("Тип: " + MainList[NumberOfElementInMainList].typeObject);
            Debug.Log("Имя: " + MainList[NumberOfElementInMainList].nameObject);
            Debug.Log("Позиция: " + MainList[NumberOfElementInMainList].position);
            Debug.Log("Ротация: " + MainList[NumberOfElementInMainList].rotation);
        }
        Debug.Log("Конец загрузки");

        MainList.Clear();
    }

    private SpawnParameters GetSpawnParameters(string typeObject)
    {
        SpawnParameters result = new SpawnParameters();

        switch (typeObject)
        {
            case "PlayerObject":
                result.dataArray = spawnConfig.PlayerObjects;
                result.parentTransform = PlayersTransform;
                break;

            case "EnemyObject":
                result.dataArray = spawnConfig.EnemyObjects;
                result.parentTransform = EnemiesTransform;
                break;

            case "FriendlyObject":
                result.dataArray = spawnConfig.FriendlyObjects;
                result.parentTransform = FriendliesTransform;
                break;

            case "ConstructionObject":
                result.dataArray = spawnConfig.ConstructionObjects;
                result.parentTransform = ConstructionsTransform;
                break;

            case "EnvironmentObject":
                result.dataArray = spawnConfig.EnvironmentObjects;
                result.parentTransform = EnvironmentsTransform;
                break;
        }

        return result;
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

    private void SpawnObject(int numberOfElementInMainList, int numberOfElementInDataArray, SpawnParameters spawnParameters)
    {
        while (spawnParameters.dataArray[numberOfElementInDataArray].ObjectName != MainList[numberOfElementInMainList].nameObject)
        {
            numberOfElementInDataArray++;
        }
        if (spawnParameters.dataArray[numberOfElementInDataArray].ObjectName == MainList[numberOfElementInMainList].nameObject)
        {
            var newObject = Instantiate(spawnParameters.dataArray[numberOfElementInDataArray].ObjectTransform, MainList[numberOfElementInMainList].position, MainList[numberOfElementInMainList].rotation, spawnParameters.parentTransform);
            newObject.localScale = MainList[numberOfElementInMainList].scale;
        }
    }

    public Transform[] ObjectContainers;

    public void AddChanges()
    {
        childs.Clear();
        SaveContainer newScene = new();
        for (int containerIndex = 0; containerIndex < ObjectContainers.Length; containerIndex++)
        {
            foreach (Transform child in ObjectContainers[containerIndex])
            {
                childs.Add(child);
            }
        }
        EndSave();
    }

    public void SaveScene()
    {
        childs.Clear();
        jString = File.ReadAllText("Assets/" + jsonFile);
        Debug.Log("Пытаемся сохраниться");
        SaveContainer newScene = new();
        foreach(Transform child in transform)
        {
            childs.Add(child);
        }
        EndSave();
    }

    private void EndSave()
    {
        Debug.Log("Всего детей: " + childs.Count);
        for (int numberOfChild = 0; numberOfChild < childs.Count; numberOfChild++)
        {
            if (childs[numberOfChild].TryGetComponent<EnvironmentData>(out EnvironmentData environmentObject))
            {
                SaveEnvironmentObject(environmentObject);
            }
            else if (childs[numberOfChild].TryGetComponent<PlayerData>(out PlayerData playerObject))
            {
                SavePlayerObject(playerObject);
            }
            else if (childs[numberOfChild].TryGetComponent<EnemyData>(out EnemyData enemyObject))
            {
                SaveEnemyObject(enemyObject);
            }
            else if (childs[numberOfChild].TryGetComponent<ConstructionData>(out ConstructionData constructionObject))
            {
                SaveConstructionObject(constructionObject);
            }
            else if (childs[numberOfChild].TryGetComponent<FriendlyData>(out FriendlyData friendlyObject))
            {
                SaveFriendlyObject(friendlyObject);
            }
            else Debug.Log("Этот объект не подпадает под категории, его имя: " + childs[numberOfChild].name);
        }
        File.WriteAllText("Assets/" + jsonFile, jString);
        Debug.Log("Завершаем сохранение");
        childs.Clear();
        MainList.Clear();
    }

    private void SaveFriendlyObject(FriendlyData friendlyObject)
    {
        int NumberOfFriendlyObject = 0;
        while (spawnConfig.FriendlyObjects[NumberOfFriendlyObject].objectName != friendlyObject.objectName && NumberOfFriendlyObject < spawnConfig.PlayerObjects.Length)
        {
            NumberOfFriendlyObject++;
        }
        Debug.Log("Имя объекта: " + friendlyObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "FriendlyObject";
        thisObject.nameObject = friendlyObject.objectName;
        thisObject.position = friendlyObject.transform.position;
        thisObject.rotation = friendlyObject.transform.rotation;
        thisObject.scale = friendlyObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveConstructionObject(ConstructionData constructionObject)
    {
        int NumberOfConstructionObject = 0;
        while (spawnConfig.ConstructionObjects[NumberOfConstructionObject].objectName != constructionObject.objectName && NumberOfConstructionObject < spawnConfig.PlayerObjects.Length)
        {
            NumberOfConstructionObject++;
        }
        Debug.Log("Имя объекта: " + constructionObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "ConstructionObject";
        thisObject.nameObject = constructionObject.objectName;
        thisObject.position = constructionObject.transform.position;
        thisObject.rotation = constructionObject.transform.rotation;
        thisObject.scale = constructionObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveEnemyObject(EnemyData enemyObject)
    {
        int NumberOfEnemyObject = 0;
        while (spawnConfig.EnemyObjects[NumberOfEnemyObject].objectName != enemyObject.objectName && NumberOfEnemyObject < spawnConfig.PlayerObjects.Length)
        {
            NumberOfEnemyObject++;
        }
        Debug.Log("Имя объекта: " + enemyObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnemyObject";
        thisObject.nameObject = enemyObject.objectName;
        thisObject.position = enemyObject.transform.position;
        thisObject.rotation = enemyObject.transform.rotation;
        thisObject.scale = enemyObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SavePlayerObject(PlayerData playerObject)
    {
        int NumberOfPlayerObject = 0;
        while (spawnConfig.PlayerObjects[NumberOfPlayerObject].objectName != playerObject.objectName && NumberOfPlayerObject < spawnConfig.PlayerObjects.Length)
        {
            NumberOfPlayerObject++;
        }
        Debug.Log("Имя объекта: " + playerObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "PlayerObject";
        thisObject.nameObject = playerObject.objectName;
        thisObject.position = playerObject.transform.position;
        thisObject.rotation = playerObject.transform.rotation;
        thisObject.scale = playerObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }

    private void SaveEnvironmentObject(EnvironmentData environmentObject)
    {
        int NumberOfEnvironmentObject = 0;
        while (spawnConfig.EnvironmentObjects[NumberOfEnvironmentObject].objectName != environmentObject.objectName && NumberOfEnvironmentObject < spawnConfig.PlayerObjects.Length)
        {
            NumberOfEnvironmentObject++;
        }
        Debug.Log("Имя объекта: " + environmentObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnvironmentObject";
        thisObject.nameObject = environmentObject.objectName;
        thisObject.position = environmentObject.transform.position;
        thisObject.rotation = environmentObject.transform.rotation;
        thisObject.scale = environmentObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[index]);
        index++;
    }
}
