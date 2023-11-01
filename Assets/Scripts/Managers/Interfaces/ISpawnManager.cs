using CozyServer.DTS.Links;

namespace ZombieFarm.Managers.Interfaces
{
    public interface ISpawnManager
    {
        public T GetByLink<T>(ILink link) where T : UnityEngine.Object;
    }
}