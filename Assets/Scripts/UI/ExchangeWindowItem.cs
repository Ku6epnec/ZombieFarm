using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.UI
{
    public class ExchangeWindowItem : MonoBehaviour
    {
        public Image resourceImage;
        [SerializeField] private TextMeshProUGUI amountText;

        [Header("Background")]
        [SerializeField] private Image background;
        [SerializeField] private Sprite activeSprite;

        [HideInInspector] public LinkToResource link;
        private IResourceManager resourceManager;
        private int amount;

        public Button button { get; private set; }

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
            UpdateResourceAmount(link, 0);
            SetInteractable(amount > 0);
        }

        public void UpdateResourceAmount(LinkToResource _, int amountChanged)
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

        public void SetInteractable(bool interactable)
        {
            button.interactable = interactable;
            background.overrideSprite = interactable == true ? activeSprite : null;
        }
    }
}
