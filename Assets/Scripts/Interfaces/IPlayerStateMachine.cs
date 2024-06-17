using UnityTools.Runtime.StatefulEvent;
using ZombieFarm.Views.Player;

namespace ZombieFarm.Interfaces
{
    public interface IPlayerStateMachine
    {
        IStatefulEvent<PlayerState> CurrentPlayerState {get; }
        void Update();
        void SetDeath();
    }
}