using CozyServer.DTS.Links;

namespace ZombieFarm.Managers.Interfaces
{
    public interface IConfigManager
    {
        GameSettings GameSettings {get; }
        T GetByLink<T>(ILink link) where T : UnityEngine.Object;
    }
}