using System;

namespace ZombieFarm.Managers.CacheManager
{
    public interface IUnityInvocations
    {
        event Action OnUpdateEvent;
        event Action OnApplicationPauseEvent;
        event Action OnDestroyEvent;
    }
}
