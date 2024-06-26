using ZombieFarm.Views.Player;
using UnityTools.Runtime.StatefulEvent;
using UnityEngine;

namespace ZombieFarm.Managers.Interfaces
{
    public interface IPlayerManager
    {
        IStatefulEvent<PlayerState> CurrentPlayerState { get; }
        IItemWithHealthBar PlayerItemWithHealthBar { get; }
        Transform PlayerTransform { get; }
        float CurrentMotionSpeed { get; }
        void RegisterDamage(float damageValue);
    }
}
