
using ZombieFarm.Interfaces;

namespace ZombieFarm.Managers.Interfaces
{
    public interface IUIManager
    {
        IJoystick Joystick { get; }
        public void OpenPanel(string type);
        public void ClosePanel();
    }
}
