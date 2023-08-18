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
        Debug.Log("�������� �����������");
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
                newScene.EnvironmentDictionary.Add(environmentObject.objectName, environmentObject.transform);
            }
            else if (childs[i].TryGetComponent<IPlayerObject>(out IPlayerObject playerObject))
            {
                Debug.Log("��� �������: " + playerObject.objectName);
                Debug.Log("����� �������: " + newScene.playerObjects.Length);
                newScene.playerObjects = new TestPlayerObject[newScene.playerObjects.Length];
                Debug.Log("����� �������: " + newScene.playerObjects.Length);
                newScene.playerObjects[0].name = playerObject.objectName;
                newScene.playerObjects[0].position = playerObject.transform.position;
                newScene.playerObjects[0].rotation = playerObject.transform.rotation;
                newScene.PlayerDictionary.Add(playerObject.objectName, playerObject.transform);
                //Debug.Log("����� ������-����� " + playerObject.objectName);
                //Debug.Log("��� ��������� " + playerObject.transform);
                //Debug.Log("��� ������� " + playerObject.transform.position);
                //newScene.PlayerDictionary.Add(playerObject.objectName, playerObject.transform);
                //Debug.Log("��������� " + newScene);
                //Debug.Log("������ ���������� " + newScene.PlayerDictionary);
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
            else Debug.Log("���� ������ �� ��������� ��� ���������, ��� ���: " + childs[i].name);
        }
        //string newJsonConteiner = JsonUtility.ToJson(newScene);
        Debug.Log("��� �� �������� �������� � Json: " + JsonUtility.ToJson(newScene.playerObjects[0]));
        File.WriteAllText("Assets/jsonContainer", JsonUtility.ToJson(newScene.playerObjects[0]));
        Debug.Log("��������� ����������");
    }
}
