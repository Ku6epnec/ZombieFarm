using UnityEngine;
using ZombieFarm.Managers;

namespace ZombieFarm.UI
{
    public class ButtonScripts : MonoBehaviour
    {
        [SerializeField] private UIManager.WindowType uiToOpen;

        public void Open()
        {
            Root.UIManager.OpenPanel(uiToOpen);
        }

        public void CloseCurrentWindow()
        {
            Root.UIManager.ClosePanel();
        }
    }
}