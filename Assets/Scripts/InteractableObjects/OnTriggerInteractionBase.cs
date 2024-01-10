using System;
using UnityEngine;

namespace ZombieFarm.InteractableObjects
{
    public class OnTriggerInteractionBase : MonoBehaviour, IItemWithHealthBar
    {
        [SerializeField] private float timeToOpen = 3f;

        private bool isOpening = false;
        private float currentTime;

        public float MaxHealthBarValue => timeToOpen;

        public event Action<float> OnRefreshProgress = (lostProgress) => { };
        public event Action OnResetProgress = () => { };
        public event Action<bool> OnRefreshProgressBarState = (isActive) => { };

        protected virtual void Awake()
        {
            currentTime = timeToOpen;
            OnRefreshProgressBarState(false);
        }

        private void Update()
        {
            if (isOpening)
            {
                currentTime -= Time.deltaTime;
                OnRefreshProgress(currentTime);

                if (currentTime <= 0)
                {
                    FinishProcess();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                OnRefreshProgressBarState(true);
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
        protected virtual void FinishProcess()
        {
            OnRefreshProgressBarState(false);
            isOpening = false;
        }

        private void ResetProgress()
        {
            OnResetProgress();
            currentTime = timeToOpen;
            isOpening = false;
            OnRefreshProgressBarState(false);
        }
    }
}