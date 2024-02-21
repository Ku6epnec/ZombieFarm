using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombieFarm.UI
{
    public class TransitionWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private GameObject sceneChangeFarm;
        [SerializeField] private GameObject sceneChangeAny;
        [SerializeField] private GameObject sceneChangeDesert;

        void Start()
        {
            gameObject.SetActive(false);

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    sceneChangeFarm.SetActive(false);
                    break;

                case 1:
                    sceneChangeAny.SetActive(false);
                    break;

                case 2:
                    sceneChangeDesert.SetActive(false);
                    break;
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
