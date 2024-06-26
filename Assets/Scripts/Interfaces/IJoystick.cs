using UnityEngine;
using UnityTools.Runtime.StatefulEvent;

namespace ZombieFarm.Interfaces
{
    public interface IJoystick
    {
        IStatefulEvent<bool> IsActive {get; }
        Vector3 GetCurrentMoveCommand();
    }
}
