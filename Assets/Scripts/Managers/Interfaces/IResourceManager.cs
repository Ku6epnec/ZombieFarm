using CozyServer.DTS.Links;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Config.Links;

namespace ZombieFarm.Managers.Interfaces
{
    public interface IResourceManager
    {
        public event Action<LinkToResource> OnChangeResource;
        public List<LinkToResource> GetAllAvailableResources();
        public void AddResource(LinkToResource type, int amount);
        public bool SubtractResource(LinkToResource type, int amount);
        public int GetResourceAmount(LinkToResource type);

    }
}