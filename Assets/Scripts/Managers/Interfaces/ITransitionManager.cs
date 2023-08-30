namespace ZombieFarm.Managers.Interfaces
{
    public interface ITransitionManager
    {
        public void SetTransition(int sceneBuildIndex);
        public void StartTransition();
    }
}