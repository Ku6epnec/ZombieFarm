using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Views;

public class Root : MonoBehaviour
{
    private static Root instance;
    private void Awake()
    {
        instance = this;
    }

    public static Transform Player
    {
        get
        {
            return instance.RefreshPlayerMover().transform;
        }
    }

    private PlayerMover RefreshPlayerMover()
    {
        return GetComponentInChildren<PlayerMover>();
    }
}
