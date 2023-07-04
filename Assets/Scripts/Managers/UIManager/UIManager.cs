using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Managers.Interfaces;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private FloatingJoystick floatingJoystick;

    public IJoystick Joystick => floatingJoystick;

    private Dictionary<string, IUIElement> uiPanels;
    private IUIElement currentOpen;


    private void Start()
    {
        IEnumerable<IUIElement> uiPanelsObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IUIElement>();
        uiPanels = new Dictionary<string, IUIElement>();

        foreach (var element in uiPanelsObjects)
        {
            uiPanels.Add(element.ID, element);
        }
    }

    public void OpenPanel(string type)
    {
        uiPanels[type].Open();
        currentOpen = uiPanels[type];
    }

    public void ClosePanel()
    {
        currentOpen.Close();
        currentOpen = null;
    }
}
