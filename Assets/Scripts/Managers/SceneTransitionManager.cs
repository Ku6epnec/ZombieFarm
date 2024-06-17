using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class SceneTransitionManager : MonoBehaviour, ITransitionManager
    {
        public event Action OnSetNewTransitionIndex = () => { };

        [SerializeField] private UIManager.WindowType panelToOpenID;

        private int currentTransitionIndex = -1;

        public void SetTransition(int sceneBuildIndex)
        {
            currentTransitionIndex = sceneBuildIndex;

            OnSetNewTransitionIndex();
        }

        public void StartTransition()
        {
            if (currentTransitionIndex == -1) return;

            Root.UIManager.OpenPanel(panelToOpenID);
            StartCoroutine(LoadAsyncScene(currentTransitionIndex));
        }

        private IEnumerator LoadAsyncScene(int sceneBuildIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

            while (asyncLoad.isDone == false)
            {
                yield return null;
            }
        }
    }
}