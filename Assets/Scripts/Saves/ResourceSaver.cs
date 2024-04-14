using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ZombieFarm.Config.Links;
using ZombieFarm.Managers;

namespace ZombieFarm.Saves
{
    public class ResourceSaver
    {
        private const string fileName = "/resourceData.json";

        public static void Save(Dictionary<LinkToResource, int> resources)
        {
            ListOfPairsWrapper<LinkToResource, int> resourceWrapper = new ListOfPairsWrapper<LinkToResource, int>();

            foreach (LinkToResource key in resources.Keys)
            {
                resourceWrapper.dictionaryList.Add(new SerializedDictionaryElement<LinkToResource, int>(key, resources[key]));
            }

            string json = JsonUtility.ToJson(resourceWrapper);
            string filePath = Application.persistentDataPath + fileName;

            File.WriteAllText(filePath, json);
        }

        public static Dictionary<LinkToResource, int> Load()
        {
            string filePath = Application.persistentDataPath + fileName;

            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                ListOfPairsWrapper<LinkToResource, int> data = 
                    JsonUtility.FromJson<ListOfPairsWrapper<LinkToResource, int>>(fileContents);

                Dictionary<LinkToResource, int> resources = new Dictionary<LinkToResource, int>();

                foreach(SerializedDictionaryElement<LinkToResource, int> element in data.dictionaryList)
                {
                    resources.Add(element.key, element.value);
                }

                return resources;
            }

            return new Dictionary<LinkToResource, int>();
        }

        public static void ClearAll()
        {
            string filePath = Application.persistentDataPath + fileName;
            File.Delete(filePath);
        }
    }

    [Serializable]
    public class ListOfPairsWrapper<T, K>
    {
        public List<SerializedDictionaryElement<T, K>> dictionaryList;

        public ListOfPairsWrapper()
        {
            dictionaryList = new List<SerializedDictionaryElement<T, K>>();
        }
    }
}