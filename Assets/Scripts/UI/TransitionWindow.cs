using UnityEngine;

namespace ZombieFarm.UI
{
    public class TransitionWindow : MonoBehaviour, IWindow
    {
        void Start()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void TransitionToScene()
        {
            Root.TransitionManager.StartTransition();
        }
    }
}
