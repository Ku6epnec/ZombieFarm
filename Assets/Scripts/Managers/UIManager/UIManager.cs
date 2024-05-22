using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        public enum WindowType
        {
            ExchangeWindow,
            TransitionWindow,
            SceneStartWindow,
            SettingsWindow
        }

        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private List<SerializedDictionaryElement<WindowType, GameObject>> uiPanels;

        public IJoystick Joystick => floatingJoystick;

        private IWindow currentOpen;

        public void OpenPanel(WindowType type)
        {
            foreach (SerializedDictionaryElement<WindowType, GameObject> element in uiPanels)
            {
                if (element.key.Equals(type))
                {
                    if (currentOpen != null) ClosePanel();
                    currentOpen = element.value.GetComponent<IWindow>();
                    currentOpen.Open();
                    break;
                }
            }
        }

        public void ClosePanel()
        {
            currentOpen.Close();
            currentOpen = null;
        }
    }
}