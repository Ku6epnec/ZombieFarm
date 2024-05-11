using System;
using System.Collections.Generic;
using ZombieFarm.Config.Links;

namespace ZombieFarm.Managers.Interfaces
{
    public interface IResourceManager
    {
        public event Action<LinkToResource, int> OnChangeResource;
        public List<LinkToResource> GetAllAvailableResources();
        public void AddResource(LinkToResource type, int amount);
        public bool SubtractResource(LinkToResource type, int amount);
        public int GetResourceAmount(LinkToResource type);
    }
}