using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZombieFarm.Managers;

namespace ZombieFarm.UI
{
    public class TransitionWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private List<SerializedDictionaryElement<int, GameObject>> sceneChangeItems;

        void Start()
        {
            gameObject.SetActive(false);
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            foreach(SerializedDictionaryElement<int, GameObject> element in sceneChangeItems)
            {
                if (activeSceneIndex == element.key)
                {
                    element.value.SetActive(false);
                    break;
                }
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void TransitionToScene(int sceneIndexToTranistion)
        {
            Root.TransitionManager.SetTransition(sceneIndexToTranistion);
            Root.TransitionManager.StartTransition();
        }
    }
}