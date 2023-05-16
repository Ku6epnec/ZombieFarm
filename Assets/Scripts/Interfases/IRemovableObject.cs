using System;

namespace ZombieFarm.Interfaces
{
    public interface IRemovableObject
    {
        event Action<bool> OnDestroyProcess;
    }
}
