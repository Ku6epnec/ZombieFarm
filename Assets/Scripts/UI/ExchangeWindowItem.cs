using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.UnityRuntime.UI.Element;
using UnityTools.UnityRuntime.UI.ElementSet;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.UI
{
    public class ExchangeWindowItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountText;
        public Image resourceImage;

        [HideInInspector] public Button button;
        [HideInInspector] public LinkToResource link;

        private IResourceManager resourceManager;
        private int amount;

        public int Amount
        {
            get
            {
                return amount;
            }
            private set
            {
                amount = value;
                amountText.text = amount.ToString();
            }
        }

        public void SetUp(LinkToResource linkToResource)
        {
            link = linkToResource;
            resourceImage.sprite = Root.ConfigManager.GetByLink<Resource>(link).sprite;
            button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            resourceManager = Root.ResourceManager;
            resourceManager.OnChangeResource += UpdateResourceAmount;
            UpdateResourceAmount(link);
            if (Amount == 0)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }

        public void UpdateResourceAmount(LinkToResource _)
        {
            Amount = resourceManager.GetResourceAmount(link);
        }

        public int GetExchangeRate(LinkToResource other)
        {
            var foundResource = Root.ConfigManager.GetByLink<Resource>(link).worthResources.Find(s => s.linkToOtherResource == other);
            if (foundResource.linkToOtherResource.HasValue == false)
            {
                return -1;
            }
            return foundResource.otherWorth / foundResource.thisWorth;
        }
    }
}
