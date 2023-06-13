using ZombieFarm.Views.Player;

public interface IViewManager
{
    abstract PlayerView GetPlayerView();
    abstract InteractiveArea GetInteractiveArea();
}
