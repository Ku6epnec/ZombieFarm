using System;
using UnityEngine;

namespace ZombieFarm.Managers 
{
    [Serializable]
    public struct SerializedDictionaryElement
    {
        public string key;
        public GameObject value;
    }
}