using UnityEngine;
using ZombieFarm.Managers;
using ZombieFarm.Managers.CacheManager;
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
    [SerializeField] SceneTransitionManager transitionManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] CacheManager cacheManager; 

    private static Root instance;

    private void Awake()
    {
        instance = this;
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
