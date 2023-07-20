using UnityEngine;

namespace ZombieFarm.InteractableObjects
{
    public class Cage : OnTriggerInteractionWithVFX
    {
        [SerializeField] AnimalFollow animal;

        protected override void Awake()
        {
            base.Awake();
            progressBar.ProcessCompleted += Free;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            progressBar.ProcessCompleted -= Free;
        }

        private void Free()
        {
            animal.StartFollowing();
        }
    }
}
