using System;
using UnityEngine;

namespace ZombieFarm.InteractableObjects
{
    public class Plant : OnTriggerInteractionBase
    {
        internal event Action OnPlantCollected = () => { };
        
        [SerializeField] private ZombieFarm.Loot.Loot loot;

        protected override void FinishProcess()
        {
            base.FinishProcess();

            loot.AddToInventory();

            gameObject.SetActive(false);

            OnPlantCollected();
            
            ResetProgress();
        }
    }
}
