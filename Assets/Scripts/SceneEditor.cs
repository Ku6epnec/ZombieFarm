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
            while (spawnConfig.PlayerObjects[j].objectName != MainList[i].nameObject && spawnConfig.PlayerObjects.Length < j)
            {
                j++;
            }    
            if (spawnConfig.PlayerObjects[j].objectName == MainList[i].nameObject)
            {
                Instantiate(spawnConfig.PlayerObjects[j].playerView, MainList[i].position, MainList[i].rotation);
            }
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
    }

    private void SaveFriendlyObject(IFriendlyObject friendlyObject)
    {
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
