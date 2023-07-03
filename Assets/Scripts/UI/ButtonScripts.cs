using UnityEngine;

public class ButtonScripts : MonoBehaviour
{
    public void Open(string name)
    {
        Root.UIManager.OpenPanel(name);
    }

    public void CloseCurrentWindow()
    {
        Root.UIManager.ClosePanel();
    }
}
