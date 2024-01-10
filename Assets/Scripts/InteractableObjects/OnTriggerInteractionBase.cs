using System;
using UnityEngine;

namespace ZombieFarm.InteractableObjects
{
    public class OnTriggerInteractionBase : MonoBehaviour, IItemWithHealthBar
    {
        [SerializeField] private float timeToOpen = 3f;
        [SerializeField] protected ProgressBar progressBar;

        private bool isOpening = false;
        private float currentTime;

        public event Action<float> OnRefreshProgress = (lostProgress) => { };

        protected virtual void Awake()
        {
            currentTime = timeToOpen;
            progressBar.InitSlider(timeToOpen, this);
            progressBar.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isOpening)
            {
                currentTime -= Time.deltaTime;
                OnRefreshProgress(currentTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                progressBar.gameObject.SetActive(true);
                isOpening = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                ResetProgress();
            }
        }

        private void ResetProgress()
        {
            progressBar.ResetProgress();
            currentTime = timeToOpen;
            isOpening = false;
            progressBar.gameObject.SetActive(false);
        }
    }
}