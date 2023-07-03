using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
