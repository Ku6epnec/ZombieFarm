using System.Collections;
using UnityEngine;

namespace ZombieFarm.InteractableObjects
{
    public class OnTriggerInteractionWithVFX : OnTriggerInteractionBase
    {
        [SerializeField] private Transform objectModel;
        [SerializeField] private ParticleSystem disappearVFX;

        protected override void Awake()
        {
            base.Awake();
            progressBar.OnProcessCompleted += Open;
        }

        protected virtual void OnDestroy()
        {
            progressBar.OnProcessCompleted -= Open;
        }

        private void Open()
        {
            objectModel.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(false);

            float vfxDuration = disappearVFX.main.startLifetime.constantMax;
            StartCoroutine(DestroyTimer(vfxDuration));
        }

        private IEnumerator DestroyTimer(float destroyTimeout)
        {
            disappearVFX.gameObject.SetActive(true);

            yield return new WaitForSeconds(destroyTimeout);

            gameObject.SetActive(false);
        }
    }
}