using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Interfaces
{
    public interface IPlayerView
    {
        void Init(IPlayerProfile playerProfile, IPlayerManager playerManager);
        void RefreshHealthBar();
    }
}