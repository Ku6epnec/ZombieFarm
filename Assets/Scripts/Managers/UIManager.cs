using System;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private FloatingJoystick floatingJoystick;

        public IJoystick Joystick => floatingJoystick;
    }
}
