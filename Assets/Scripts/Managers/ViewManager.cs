using UnityEngine;
using ZombieFarm.Views.Player;

public class ViewManager : MonoBehaviour, IViewManager
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private InteractiveArea _interactiveArea;

    public PlayerView GetPlayerView() => _playerView;
    public InteractiveArea GetInteractiveArea() => _interactiveArea;
}
