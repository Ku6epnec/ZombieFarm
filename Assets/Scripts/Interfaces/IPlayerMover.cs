using UnityTools.Runtime.StatefulEvent;
using ZombieFarm.Views.Player;

namespace ZombieFarm.Interfaces
{
    public interface IPlayerMover
    {
        float CurrentMotionSpeed {get; }
    }
}