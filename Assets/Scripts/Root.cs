using System.Collections.Generic;
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
    [SerializeField] UIManager uiManager;

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

    public static IUIManager UIManager
    {
        get 
        {
            return instance.uiManager;
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
<<<<<<< HEAD
    
    public static ConfigManager ConfigManager
=======
    public static IConfigManager ConfigManager
>>>>>>> a455c69 (resource and config managers)
    {
        get
        {
            return instance.configManager;
        }
    }
    public static UIManager UIManager
    {
        get
        {
            return instance.uiManager;
        }
    }
}
