using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.UI
{
    public class ResourceList : MonoBehaviour
    {
        [SerializeField] private GameObject contents;
        [SerializeField] private GameObject placeholderPrefab;

        private Dictionary<LinkToResource, ResourceItem> resourceToUI;
        private List<ResourceItem> emptyPlaceholders;
        private IResourceManager resourceManager;

        private void Start()
        {
            resourceManager = Root.ResourceManager;
            resourceManager.OnChangeResource += UpdateUI;
            UpdateUIOnStart();
        }

        private void OnDestroy()
        {
            resourceManager.OnChangeResource -= UpdateUI;
        }

        private void UpdateUIOnStart()
        {
            resourceToUI = new Dictionary<LinkToResource, ResourceItem>();
            emptyPlaceholders = contents.GetComponentsInChildren<ResourceItem>(true).ToList();

            foreach (ResourceItem item in emptyPlaceholders)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void UpdateUI(LinkToResource linkToResource)
        {
            if (resourceToUI.ContainsKey(linkToResource))
            {
                resourceToUI[linkToResource].amountText.text = resourceManager.GetResourceAmount(linkToResource).ToString();
            }
            else
            {
                ResourceItem placeholder;
                if (emptyPlaceholders.Count != 0)
                {
                    placeholder = emptyPlaceholders[0];
                    emptyPlaceholders.RemoveAt(0);
                }
                else
                {
                    placeholder = Instantiate(placeholderPrefab).GetComponent<ResourceItem>();
                    placeholder.transform.parent = contents.transform;
                }

                Resource resourceInfo = Root.ConfigManager.GetByLink<Resource>(linkToResource);
                placeholder.image.sprite = resourceInfo.sprite;
                placeholder.nameText.text = resourceInfo.displayName;
                placeholder.amountText.text = resourceManager.GetResourceAmount(linkToResource).ToString();
                placeholder.gameObject.SetActive(true);

                resourceToUI.Add(linkToResource, placeholder);
            }
        }
    }
}