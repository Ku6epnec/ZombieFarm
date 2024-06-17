using UnityEngine;
using ZombieFarm.Managers;
using ZombieFarm.Managers.CacheManager;
using ZombieFarm.Managers.Interfaces;
using ZombieFarm.Views.Player;

public class Root : MonoBehaviour
{
    [SerializeField] ZombieManager zombieManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] ResourceManager resourceManager;
    [SerializeField] ConfigManager configManager;
    [SerializeField] SceneTransitionManager transitionManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] CacheManager cacheManager; 

    private static Root instance;

    private void Awake()
    {
        instance = this;
    }

    public static IEnemyManager ZombieManager
    {
        get
        {
            return instance.zombieManager;
        }
    }

    public static IPlayerManager PlayerManager
    {
        get
        {
            return instance.playerManager;
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

    public static ITransitionManager TransitionManager
    {
        get
        {
            return instance.transitionManager;
        }
    }

    public static Camera Camera
    {
        get
        {
            return instance.mainCamera;
        }
    }

    public static ICacheManager CacheManager
    {
        get
        {
            return instance.cacheManager;
        }
    }
}
