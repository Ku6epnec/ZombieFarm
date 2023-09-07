using System;

namespace ZombieFarm.Managers.Interfaces
{
    public interface ITransitionManager
    {
        event Action OnSetNewTransitionIndex;
        void SetTransition(int sceneBuildIndex);
        void StartTransition();
    }
}