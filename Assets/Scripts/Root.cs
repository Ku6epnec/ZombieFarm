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
    [SerializeField] Spawner spawner;

    private static Root instance;

    private void Awake()
    {
        instance = this;
        if (spawnConfig == null) spawnConfig = Resources.Load("SceneTestSpawnConfig") as SpawnConfig;
        Spawner.Spawn(spawnConfig, transform);
        player = spawnConfig.PlayerObjects[0].playerMover;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Сохраняем данные объектов");
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
    public static SpawnConfig SpawnConfigs
    {
        get
        {
            return instance.spawnConfig;
        }
    }
    public static Spawner Spawner
    {
        get
        {
            return instance.spawner;
        }
    }
}
