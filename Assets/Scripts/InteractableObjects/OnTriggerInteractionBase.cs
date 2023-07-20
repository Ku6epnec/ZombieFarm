using UnityEngine;

namespace ZombieFarm.InteractableObjects
{
    public class OnTriggerInteractionBase : MonoBehaviour
    {
        [SerializeField] private float timeToOpen = 3f;
        [SerializeField] protected ProgressBar progressBar;

        private bool isOpening = false;
        private float currentTime;

        protected virtual void Awake()
        {
            currentTime = timeToOpen;
            progressBar.InitSlider(timeToOpen);
            progressBar.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isOpening)
            {
                currentTime -= Time.deltaTime;
                progressBar.RefreshProgress(currentTime);
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