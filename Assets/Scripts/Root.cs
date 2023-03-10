using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Views;
using ZombieFarm.Managers.Interfaces;
using ZombieFarm.Managers;

public class Root : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] ZombieManager zombieManager;

    private static Root instance;

    private void Awake()
    {
        instance = this;
    }

    public static Transform Player
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
}
