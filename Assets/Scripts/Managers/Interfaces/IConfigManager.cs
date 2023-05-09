using CozyServer.DTS.Links;

public interface IConfigManager
{
    public T GetByLink<T>(ILink link) where T : UnityEngine.Object;
}