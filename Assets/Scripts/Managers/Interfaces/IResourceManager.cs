using CozyServer.DTS.Links;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Config.Links;

public interface IResourceManager
{
    public event Action<LinkToResource> OnChangeResource;
    public void AddResource(LinkToResource type, int amount);
    public bool SubtractResource(LinkToResource type, int amount);
    public int GetResourceAmount(LinkToResource type);

}
