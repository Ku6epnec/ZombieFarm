using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFarm.Interfaces
{ 
    public interface ICacheStorage<T> : ICacheStorage
    {
        T Value { get; set; }
    }

    public interface ICacheStorage
    {
        bool HasValue { get; }
        void Clear();
    }
}
