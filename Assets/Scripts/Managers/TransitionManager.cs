using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class TransitionManager : MonoBehaviour, ITransitionManager
    {
        [SerializeField] private string panelToOpenID = "SceneStartWindow";

        private int currentTransitionIndex = -1;

        public void SetTransition(int sceneBuildIndex)
        {
            currentTransitionIndex = sceneBuildIndex;
        }

        public void StartTransition()
        {
            if (currentTransitionIndex == -1) return;

            Root.UIManager.OpenPanel(panelToOpenID);
            Debug.Log("UI should be opened");
            StartCoroutine(LoadAsyncScene(currentTransitionIndex));
        }
        private IEnumerator LoadAsyncScene(int sceneBuildIndex)
        {
            Debug.Log("Coroutine should be start");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}