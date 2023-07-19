using UnityEngine;
using ZombieFarm.Views.Player;

public class ViewManager : MonoBehaviour, IViewManager
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private InteractiveArea _interactiveArea;
    [SerializeField] SpawnConfig spawnConfig;
    public PlayerView GetPlayerView() => _playerView;
    public InteractiveArea GetInteractiveArea() => _interactiveArea;
    private void Awake()
    {
        //if (spawnConfig == null) spawnConfig = Resources.Load("SceneTestSpawnConfig") as SpawnConfig;
        //_playerView = spawnConfig.PlayerObjects[0].playerView;
    }
}
