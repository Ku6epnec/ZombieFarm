using System;
using UnityEngine;

namespace ZombieFarm.Managers 
{
    [Serializable]
    public struct SerializedDictionaryElement<T, K>
    {
        public T key;
        public K value;
    }
}