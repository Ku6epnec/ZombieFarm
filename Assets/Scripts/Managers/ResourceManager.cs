using CozyServer.DTS.Links;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityTools.Runtime.Links;
using ZombieFarm.Config.LinkTargets;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    Dictionary<ILink, int> resourceAmount;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        resourceAmount = new Dictionary<ILink, int>();

    }

    public void AddResource(ILink type, int amount)
    {
        resourceAmount[type] += amount;
    }
    public bool SubtractResource(ILink type, int amount)
    {
        if (resourceAmount[type] - amount < 0)
        {
            return false;
        }
        else
        {
            resourceAmount[type] -= amount;
            return true;
        }
    }

    public int GetResourceAmount(ILink type)
    {
        return resourceAmount[type];
    }

}
