using UnityEngine;
using ZombieFarm.Managers;
using ZombieFarm.Managers.Interfaces;
using ZombieFarm.Views.Player;

public class Root : MonoBehaviour
{
    [SerializeField] PlayerMover player;
    [SerializeField] ZombieManager zombieManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] ViewManager viewManager;
    [SerializeField] ResourceManager resourceManager;
    [SerializeField] ConfigManager configManager;
    [SerializeField] SpawnConfig spawnConfig;

    private static Root instance;

    private void Awake()
    {
        if (spawnConfig == null) spawnConfig = Resources.Load("SceneTestSpawnConfig") as SpawnConfig;
        instance = this;
        if (spawnConfig != null) Spawner.Spawn(spawnConfig, transform);     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

        }
    }
    public static PlayerMover Player
    {
        get
        {
            return instance.player;
        }
    }

    public static IEnemyManager ZombieManager
    {
        get
        {
            return instance.zombieManager;
        }
    }

    public static IViewManager ViewManager
    {
        get
        {
            return instance.viewManager;
        }
    }
    
    public static IResourceManager ResourceManager
    {
        get
        {
            return instance.resourceManager;
        }
    }
    public static IConfigManager ConfigManager
    {
        get
        {
            return instance.configManager;
        }
    }
    public static IUIManager UIManager
    {
        get
        {
            return instance.uiManager;
        }
    }

    
}
