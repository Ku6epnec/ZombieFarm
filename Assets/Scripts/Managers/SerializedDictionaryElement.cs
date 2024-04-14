using System;

namespace ZombieFarm.Managers
{
    [Serializable]
    public struct SerializedDictionaryElement<T, K>
    {
        public T key;
        public K value;

        public SerializedDictionaryElement(T key, K value)
        {
            this.key = key;
            this.value = value;
        }
    }
}