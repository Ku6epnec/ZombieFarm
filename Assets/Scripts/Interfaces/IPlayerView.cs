using UnityEngine;
using UnityTools.Runtime.StatefulEvent;

namespace ZombieFarm.Interfaces
{
    public interface IPlayerView
    {
        void Init(IPlayerProfile playerProfile);
        void RefreshHealthBar();
    }
}