namespace ZombieFarm.InteractableObjects
{
    public class ChestBase : OnTriggerInteractionWithVFX
    {
        private Loot.Loot loot;

        protected override void Awake()
        {
            base.Awake();
            loot = GetComponent<Loot.Loot>();
        }

        protected override void Open()
        {
            loot.AddToInventory();
            base.Open();
        }
    }
}
