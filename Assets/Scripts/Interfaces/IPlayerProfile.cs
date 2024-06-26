using System;
using UnityTools.Runtime.StatefulEvent;

namespace ZombieFarm.Interfaces
{
    public interface IPlayerProfile
    {
        event Action OnDie;
        IStatefulEvent<float> CurrentHealth {get; }
        
        void RegisterDamage(float damageValue);
    }
}
