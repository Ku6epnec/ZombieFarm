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
            loot = GetComponent<Loot.Loot>();
        }

        protected override void FinishProcess()
        {
            loot.AddToInventory();
            base.FinishProcess();
            Free();
        }

        private void Free()
        {
            animal.StartFollowing();
        }
    }
}
