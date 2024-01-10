namespace ZombieFarm.InteractableObjects
{
    public class ChestBase : OnTriggerInteractionWithVFX, IItemWithHealthBar
    {
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
        }
    }
}
