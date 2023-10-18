namespace ZombieFarm.Loot
{
    public class OnDisableLoot : Loot
    {
        private void OnDisable()
        {
            AddToInventory();
        }
    }
}
