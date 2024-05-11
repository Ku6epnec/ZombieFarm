using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;

namespace ZombieFarm.UI
{
    public class ResourceAdditionDisplay : MonoBehaviour
    {
        [SerializeField] private int secondsToShow = 3;
        [SerializeField] private List<ResourceItem> resourcesDisplayWindows;

        private Queue<ResourceWithAmount> resourcesInLine;
        private Action<ResourceItem> onPlaceholderEmptied = (resourceItem) => { };

        private void Start()
        {
            Root.ResourceManager.OnChangeResource += ResourceChanged;
            onPlaceholderEmptied += OnPlaceholderEmptied;
            resourcesInLine = new Queue<ResourceWithAmount>();
            foreach (var resource in resourcesDisplayWindows)
            {
                resource.gameObject.SetActive(false);
            }
        }

        private void OnPlaceholderEmptied(ResourceItem item)
        {
            if (resourcesInLine.Count == 0)
            {
                return;
            }
            ResourceWithAmount resource = resourcesInLine.Dequeue();
            DisplayResource(resource.resource, resource.amount, item);
        }

        private void ResourceChanged(LinkToResource linkToResource, int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            ResourceItem itemToDisplay = null;
            foreach (ResourceItem resource in resourcesDisplayWindows)
            {
                if (!resource.gameObject.activeSelf)
                {
                    itemToDisplay = resource;
                    break;
                }
            }

            if (itemToDisplay != null)
            {
                DisplayResource(linkToResource, amount, itemToDisplay);
            }
            else
            {
                resourcesInLine.Enqueue(new ResourceWithAmount(linkToResource, amount));
            }
        }

        private void DisplayResource(LinkToResource linkToResource, int amount, ResourceItem resourceItem)
        {
            Resource resource = Root.ConfigManager.GetByLink<Resource>(linkToResource);
            resourceItem.image.sprite = resource.sprite;
            resourceItem.amountText.text = $"+{amount}";
            resourceItem.gameObject.SetActive(true);

            StartCoroutine(WaitingForAction(resourceItem));
        }

        private IEnumerator WaitingForAction(ResourceItem resourceItem)
        {
            yield return new WaitForSeconds(secondsToShow);

            resourceItem.gameObject.SetActive(false);
            onPlaceholderEmptied(resourceItem);
        }

        private class ResourceWithAmount
        {
            public LinkToResource resource;
            public int amount;

            public ResourceWithAmount(LinkToResource resource, int amount)
            {
                this.resource = resource;
                this.amount = amount;
            }
        }
    }
}
