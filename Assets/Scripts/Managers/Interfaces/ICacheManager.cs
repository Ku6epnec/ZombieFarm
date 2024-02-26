using System;
using ZombieFarm.Interfaces;

namespace ZombieFarm.Managers.Interfaces
{
    public interface ICacheManager
    {
        event Action OnUpdateEvent;
        event Action OnApplicationPauseEvent;
        event Action OnDestroyEvent;

        ICacheStorage<T> GetCacheStorage<T>(bool rememberBetweenSessions);
    }
}
