using UnityEngine;
using ZombieFarm.AI;

namespace ZombieFarm.InteractableObjects
{
    public class Cage : OnTriggerInteractionWithVFX
    {
        [SerializeField] AnimalFollow animal;

        protected override void Awake()
        {
            base.Awake();
            progressBar.OnProcessCompleted += Free;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            progressBar.OnProcessCompleted -= Free;
        }

        private void Free()
        {
            animal.StartFollowing();
        }
    }
}
