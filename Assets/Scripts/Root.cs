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
}
