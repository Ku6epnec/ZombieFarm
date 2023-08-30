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
        Debug.Log("�������� �����������");
        string jsonContainer = File.ReadAllText("Assets/jsonContainer");
        string[] StringObjects = jsonContainer.Split("}{");
        Debug.Log("������: " + jsonContainer);
        for (int i = 0; i < StringObjects.Length; i++)
        {
            if (i == 0) StringObjects[i] = StringObjects[i] + "}";
            else if (i == StringObjects.Length - 1) StringObjects[i] = "{" + StringObjects[i];
            else StringObjects[i] = "{" + StringObjects[i] + "}";
            Debug.Log("������: " + StringObjects[i]);
            SceneObject obj = JsonUtility.FromJson<SceneObject>(StringObjects[i]);
            MainList.Add(obj);
        }
        Debug.Log("����� ������: " + MainList);

        for (int i = 0; i < MainList.Count; i++)
        {
            int j = 0;

            SpawnObject(i, j, GetSpawnParameters(MainList[i].typeObject));

            //GameObject thisChild = GameObject.Find(MainList[i].nameObject);
            //Instantiate(thisChild, MainList[i].position, MainList[i].rotation);
            if (MainList[i].typeObject == "PlayerObject")
            {
                //spawnConfig.PlayerObjects[0].objectName;
            }
            Debug.Log("���: " + MainList[i].typeObject);
            Debug.Log("���: " + MainList[i].nameObject);
            Debug.Log("�������: " + MainList[i].position);
            Debug.Log("�������: " + MainList[i].rotation);
        }
        Debug.Log("����� ��������");
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
        Debug.Log("������ �����");
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
        while (spawnParameters.dataArray[j].ObjectName != MainList[i].nameObject && spawnParameters.dataArray.Length < j)
        {
            j++;
        }
        if (spawnParameters.dataArray[j].ObjectName == MainList[i].nameObject)
        {
            Instantiate(spawnParameters.dataArray[j].ObjectTransform, MainList[i].position, MainList[i].rotation, spawnParameters.parentTransform);
        }
    }

    public void SaveScene()
    {
        Debug.Log("�������� �����������");
        SaveContainer newScene = new();
        foreach(Transform child in transform)
        {
            childs.Add(child);
        }
        Debug.Log("����� �����: " + childs.Count);
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
            else Debug.Log("���� ������ �� ��������� ��� ���������, ��� ���: " + childs[i].name);
        }

        File.WriteAllText("Assets/jsonContainer", jString);
        Debug.Log("��������� ����������");
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
                Debug.LogError("������! � ������� " + friendlyObject.name + " ����������� ��������� FriendlyData, " +
                    "�������� ������ ��������� � ��������� ������� ����������!");
            }
        }
        Debug.Log("��� �������: " + friendlyObject.objectName);
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
                Debug.LogError("������! � ������� " + constructionObject.name + " ����������� ��������� ConstructionData, " +
                    "�������� ������ ��������� � ��������� ������� ����������!");
            }
        }
        Debug.Log("��� �������: " + constructionObject.objectName);
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
                Debug.LogError("������! � ������� " + enemyObject.name + " ����������� ��������� EnemyData, " +
                    "�������� ������ ��������� � ��������� ������� ����������!");
            }
        }
        Debug.Log("��� �������: " + enemyObject.objectName);
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
                Debug.LogError("������! � ������� " + playerObject.name + " ����������� ��������� PlayerData, " +
                    "�������� ������ ��������� � ��������� ������� ����������!");
            }
        }
        Debug.Log("��� �������: " + playerObject.objectName);
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
                Debug.LogError("������! � ������� " + environmentObject.name + " ����������� ��������� EnvironmentData, " +
                    "�������� ������ ��������� � ��������� ������� ����������!");
            }
        }
        Debug.Log("��� �������: " + environmentObject.objectName);
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
