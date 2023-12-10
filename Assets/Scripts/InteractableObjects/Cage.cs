using UnityEngine;
using ZombieFarm.AI;

namespace ZombieFarm.InteractableObjects
{
    public class Cage : OnTriggerInteractionWithVFX
    {
        [SerializeField] AnimalFollow animal;

        private Loot.Loot loot;

        protected override void Awake()
        {
            base.Awake();
            progressBar.OnProcessCompleted += Free;
            loot = GetComponent<Loot.Loot>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            progressBar.OnProcessCompleted -= Free;
        }

        protected override void Open()
        {
            loot.AddToInventory();
            base.Open();
        }

        private void Free()
        {
            animal.StartFollowing();
        }
    }
}
