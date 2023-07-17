using System;
using UnityEngine;

namespace ZombieFarm.Interfaces
{
    public interface IJoystick
    {
        event Action<bool> OnPointerStateChanged;
        Vector3 GetCurrentMoveCommand();
    }
}
