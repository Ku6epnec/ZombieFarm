using CozyServer.DTS.Links;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceManager
{
    public void AddResource(ILink type, int amount);
    public bool SubtractResource(ILink type, int amount);
    public int GetResourceAmount(ILink type);

}
