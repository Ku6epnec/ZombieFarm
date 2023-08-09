using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private List<SerializedDictionaryElement> uiPanels;

        public IJoystick Joystick => floatingJoystick;

        private IUIElement currentOpen;

        public void OpenPanel(string type)
        {
            foreach (SerializedDictionaryElement element in uiPanels)
            {
                if (element.key.Equals(type))
                {
                    currentOpen = element.value.GetComponent<IUIElement>();
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