using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.UnityRuntime.UI.Element;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;

namespace ZombieFarm.UI
{
    public class ExchangeWindowItem : ElementBase
    {
        public TextMeshProUGUI amountText;

        [HideInInspector] public Button button;
        [HideInInspector] public LinkToResource link;
        [HideInInspector] public int index;

        public void SetUp(LinkToResource linkToResource, int index)
        {
            link = linkToResource;
            GetComponent<Image>().sprite = Root.ConfigManager.GetByLink<Resource>(link).sprite;
            button = GetComponent<Button>();
            this.index = index;
        }

        public int GetExchangeRate(LinkToResource other)
        {
            var foundResource = Root.ConfigManager.GetByLink<Resource>(link).worthResources.Find(s => s.linkToOtherResource == other);
            if (foundResource.linkToOtherResource.HasValue == false)
            {
                return -1;
            }
            Debug.Log(1f * foundResource.otherWorth / foundResource.thisWorth);
            return foundResource.otherWorth / foundResource.thisWorth;
        }

        private int amount;
        public int Amount
        {
            get 
            { 
                return amount; 
            }
            set
            {
                amount = value;
                amountText.text = amount.ToString();
            }
        }

    }
}
