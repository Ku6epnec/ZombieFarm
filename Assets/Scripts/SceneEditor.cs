using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

            SpawnObject(i, j, GetSpawnParameters(MainList[i].typeObject));

            Debug.Log("Тип: " + MainList[i].typeObject);
            Debug.Log("Имя: " + MainList[i].nameObject);
            Debug.Log("Позиция: " + MainList[i].position);
            Debug.Log("Ротация: " + MainList[i].rotation);
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

    private void SpawnObject(int i, int j, SpawnParameters spawnParameters)
    {
        while (spawnParameters.dataArray[j].ObjectName != MainList[i].nameObject)
        {
            j++;
        }
        if (spawnParameters.dataArray[j].ObjectName == MainList[i].nameObject)
        {
            var newObject = Instantiate(spawnParameters.dataArray[j].ObjectTransform, MainList[i].position, MainList[i].rotation, spawnParameters.parentTransform);
            newObject.localScale = MainList[i].scale;
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
        File.WriteAllText("Assets/" + jsonFile, jString);
        Debug.Log("Завершаем сохранение");
        childs.Clear();
        MainList.Clear();
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
        Debug.Log("Всего детей: " + childs.Count);
        for (int i = 0; i < childs.Count; i++)
        {
            /*    This is just sample, delete or use in code

            if (childs[i].TryGetComponent<IData>(out IData data))
            {
                if (data is EnvironmentData environmentData)
                {
                    SaveEnvironmentObject(environmentData);
                }
            }*/


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

        File.WriteAllText("Assets/" + jsonFile, jString);
        Debug.Log("Завершаем сохранение");
        childs.Clear();
        MainList.Clear();
    }

    private void SaveFriendlyObject(IFriendlyObject friendlyObject)
    {
        int i = 0;
        while (spawnConfig.FriendlyObjects[i].objectName != friendlyObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
        }
        /*if (spawnConfig.FriendlyObjects[i].objectName != friendlyObject.objectName)
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
        }*/
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

    private void SaveConstructionObject(IConstructionObject constructionObject)
    {
        int i = 0;
        while (spawnConfig.ConstructionObjects[i].objectName != constructionObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
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

    private void SaveEnemyObject(IEnemyObject enemyObject)
    {
        int i = 0;
        while (spawnConfig.EnemyObjects[i].objectName != enemyObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
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

    private void SavePlayerObject(IPlayerObject playerObject)
    {
        int i = 0;
        while (spawnConfig.PlayerObjects[i].objectName != playerObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
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

    private void SaveEnvironmentObject(IEnvironmentObject environmentObject)
    {
        int i = 0;
        while (spawnConfig.EnvironmentObjects[i].objectName != environmentObject.objectName && i < spawnConfig.PlayerObjects.Length)
        {
            i++;
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
