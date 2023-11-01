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

    public List<SceneObject> MainList = new List<SceneObject>();
    public string jString;
    public int numberObjectInMainList = 0;

    public void LoadScene()
    {
        string jsonContainer = File.ReadAllText("Assets/" + jsonFile);
        string[] stringObjects = jsonContainer.Split("}{");
        for (int numberOfObjectInJson = 0; numberOfObjectInJson < stringObjects.Length; numberOfObjectInJson++)
        {
            if (numberOfObjectInJson == 0) stringObjects[numberOfObjectInJson] = stringObjects[numberOfObjectInJson] + "}";
            else if (numberOfObjectInJson == stringObjects.Length - 1) stringObjects[numberOfObjectInJson] = "{" + stringObjects[numberOfObjectInJson];
            else stringObjects[numberOfObjectInJson] = "{" + stringObjects[numberOfObjectInJson] + "}";
            Debug.Log("Строки: " + stringObjects[numberOfObjectInJson]);
            SceneObject obj = JsonUtility.FromJson<SceneObject>(stringObjects[numberOfObjectInJson]);
            MainList.Add(obj);
        }

        for (int numberOfElementInMainList = 0; numberOfElementInMainList < MainList.Count; numberOfElementInMainList++)
        {
            int NumberOfElementInDataArray = 0;
            SpawnObject(numberOfElementInMainList, NumberOfElementInDataArray, GetSpawnParameters(MainList[numberOfElementInMainList].typeObject));
        }
        Debug.Log("Completed loading");

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
        foreach(Transform child in transform)
        {
            childs.Add(child);
        }
        EndSave();
    }

    private void EndSave()
    {
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
        Debug.Log("Completed saving process");
        childs.Clear();
        MainList.Clear();
    }

    private void SaveFriendlyObject(FriendlyData friendlyObject)
    {
        int numberOfFriendlyObject = 0;
        while (spawnConfig.FriendlyObjects[numberOfFriendlyObject].objectName != friendlyObject.objectName && numberOfFriendlyObject < spawnConfig.PlayerObjects.Length)
        {
            numberOfFriendlyObject++;
        }
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "FriendlyObject";
        thisObject.nameObject = friendlyObject.objectName;
        thisObject.position = friendlyObject.transform.position;
        thisObject.rotation = friendlyObject.transform.rotation;
        thisObject.scale = friendlyObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[numberObjectInMainList]);
        numberObjectInMainList++;
    }

    private void SaveConstructionObject(ConstructionData constructionObject)
    {
        int numberOfConstructionObject = 0;
        while (spawnConfig.ConstructionObjects[numberOfConstructionObject].objectName != constructionObject.objectName && numberOfConstructionObject < spawnConfig.PlayerObjects.Length)
        {
            numberOfConstructionObject++;
        }
        Debug.Log("Имя объекта: " + constructionObject.objectName);
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "ConstructionObject";
        thisObject.nameObject = constructionObject.objectName;
        thisObject.position = constructionObject.transform.position;
        thisObject.rotation = constructionObject.transform.rotation;
        thisObject.scale = constructionObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[numberObjectInMainList]);
        numberObjectInMainList++;
    }

    private void SaveEnemyObject(EnemyData enemyObject)
    {
        int numberOfEnemyObject = 0;
        while (spawnConfig.EnemyObjects[numberOfEnemyObject].objectName != enemyObject.objectName && numberOfEnemyObject < spawnConfig.PlayerObjects.Length)
        {
            numberOfEnemyObject++;
        }
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnemyObject";
        thisObject.nameObject = enemyObject.objectName;
        thisObject.position = enemyObject.transform.position;
        thisObject.rotation = enemyObject.transform.rotation;
        thisObject.scale = enemyObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[numberObjectInMainList]);
        numberObjectInMainList++;
    }

    private void SavePlayerObject(PlayerData playerObject)
    {
        int numberOfPlayerObject = 0;
        while (spawnConfig.PlayerObjects[numberOfPlayerObject].objectName != playerObject.objectName && numberOfPlayerObject < spawnConfig.PlayerObjects.Length)
        {
            numberOfPlayerObject++;
        }
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "PlayerObject";
        thisObject.nameObject = playerObject.objectName;
        thisObject.position = playerObject.transform.position;
        thisObject.rotation = playerObject.transform.rotation;
        thisObject.scale = playerObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[numberObjectInMainList]);
        numberObjectInMainList++;
    }

    private void SaveEnvironmentObject(EnvironmentData environmentObject)
    {
        int numberOfEnvironmentObject = 0;
        while (spawnConfig.EnvironmentObjects[numberOfEnvironmentObject].objectName != environmentObject.objectName && numberOfEnvironmentObject < spawnConfig.PlayerObjects.Length)
        {
            numberOfEnvironmentObject++;
        }
        SceneObject thisObject = new SceneObject();
        thisObject.typeObject = "EnvironmentObject";
        thisObject.nameObject = environmentObject.objectName;
        thisObject.position = environmentObject.transform.position;
        thisObject.rotation = environmentObject.transform.rotation;
        thisObject.scale = environmentObject.transform.localScale;
        MainList.Add(thisObject);
        jString += JsonUtility.ToJson(MainList[numberObjectInMainList]);
        numberObjectInMainList++;
    }
}
