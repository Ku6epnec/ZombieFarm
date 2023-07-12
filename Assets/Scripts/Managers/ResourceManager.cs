using System;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Config.Links;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class ResourceManager : MonoBehaviour, IResourceManager
    {
        Dictionary<LinkToResource, int> resourceAmount;

        public event Action<LinkToResource> OnChangeResource = (resourceName) => { };

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            resourceAmount = new Dictionary<LinkToResource, int>();
        }

        public List<LinkToResource> GetAllAvailableResources()
        {
            List<LinkToResource> allAvailableResources = new List<LinkToResource>();
            foreach(LinkToResource linkToResource in resourceAmount.Keys)
            {
                allAvailableResources.Add(linkToResource);
            }

            return allAvailableResources;
        }

        public void AddResource(LinkToResource type, int amount)
        {
            AddResourceIfMissing(type);
            resourceAmount[type] += amount;
            OnChangeResource(type);
        }

        public bool SubtractResource(LinkToResource type, int amount)
        {
            AddResourceIfMissing(type);
            if (resourceAmount[type] - amount < 0)
            {
                return false;
            }
            else
            {
                resourceAmount[type] -= amount;
                OnChangeResource(type);
                return true;
            }
        }

        public int GetResourceAmount(LinkToResource type)
        {
            if (resourceAmount.ContainsKey(type) == false)
            {
                return 0;
            }
            else
            {
                return resourceAmount[type];
            }
        }

        private void AddResourceIfMissing(LinkToResource type)
        {
            if (resourceAmount.ContainsKey(type) == false)
            {
                resourceAmount.Add(type, 0);
            }
        }
    }
}