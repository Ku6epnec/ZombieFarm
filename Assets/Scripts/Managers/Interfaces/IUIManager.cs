using ZombieFarm.Interfaces;

namespace ZombieFarm.Managers.Interfaces
{
    public interface IUIManager
    {
        IJoystick Joystick { get; }
        public void OpenPanel(UIManager.WindowType type);
        public void ClosePanel();
    }
}
