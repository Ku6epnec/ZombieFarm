using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.UI
{
    public class ExchangeWindow : MonoBehaviour, IWindow
    {
        [Header("Main elements")]
        [SerializeField] private Button exchangeButton;
        [SerializeField] private Slider amountSelection;
        [SerializeField] private GameObject upScrollListContent;
        [SerializeField] private GameObject downScrollListContent;
        [SerializeField] private GameObject optionPrefab;
        [Header("Final elements")]
        [SerializeField] private TextMeshProUGUI finalGetAmountText;
        [SerializeField] private Image finalGetImage;
        [SerializeField] private TextMeshProUGUI finalSpendAmountText;
        [SerializeField] private Image finalSpendImage;

        private List<LinkToResource> optionLinks;
        private IResourceManager resourceManager;
        private ExchangeWindowItem upItemSelected;
        private ExchangeWindowItem downItemSelected;

        private List<ExchangeWindowItem> upExchangeWindowItems;
        private List<ExchangeWindowItem> downExchangeWindowItems;

        private int downItemIteration = 1;
        private int upItemIteration = 1;

        private void Start()
        {
            resourceManager = Root.ResourceManager;

            optionLinks = new List<LinkToResource>();
            upExchangeWindowItems = new List<ExchangeWindowItem>();
            downExchangeWindowItems = new List<ExchangeWindowItem>();

            resourceManager.OnChangeResource += CheckOptions;
            amountSelection.onValueChanged.AddListener(delegate { CheckSelection(); });
            exchangeButton.onClick.AddListener(delegate { Exchange(); });

            gameObject.SetActive(false);
        }

        public void AddOne(bool add)
        {
            if (amountSelection.interactable == false) return;

            if (add == true && amountSelection.value < amountSelection.maxValue)
            {
                amountSelection.value++;
            }
            else if (add == false && amountSelection.value > amountSelection.minValue)
            {
                amountSelection.value--;
            }

            CheckSelection(); 
        }

        private void SetOptions()
        {
            optionLinks.Sort((x, y) => resourceManager.GetResourceAmount(y).CompareTo(resourceManager.GetResourceAmount(x)));

            for(int i = 0; i < upExchangeWindowItems.Count; i++)
            {
                InitOption(optionLinks[i], upExchangeWindowItems[i]);
                InitOption(optionLinks[i], downExchangeWindowItems[i]);
            }

            upItemSelected = null;
            downItemSelected = null;
            CheckSelection();
        }

        private void CheckOptions(LinkToResource obj)
        {
            if (optionLinks.Contains(obj) == false)
            {
                CreateOption(obj);
            }
        }

       private void CreateOption(LinkToResource link)
        {
            InitWindow(upScrollListContent.transform);
            InitWindow(downScrollListContent.transform);

            optionLinks.Add(link);
        
            void InitWindow(Transform scrollListContentTransform)
            {
                GameObject item = Instantiate(optionPrefab, scrollListContentTransform);
                ExchangeWindowItem itemWindow = item.GetComponent<ExchangeWindowItem>();
                InitOption(link, itemWindow);

                if (scrollListContentTransform.Equals(upScrollListContent.transform))
                {
                    upExchangeWindowItems.Add(itemWindow);
                }
                else
                {
                    downExchangeWindowItems.Add(itemWindow);
                }
            }
        }

        private void InitOption(LinkToResource link, ExchangeWindowItem itemWindow)
        {
            itemWindow.SetUp(link);
            itemWindow.button.onClick.AddListener(delegate { ScrollViewItemSelected(itemWindow); });

        }

        private void Exchange()
        {
            if (resourceManager.SubtractResource(upItemSelected.link, (int)amountSelection.value * downItemIteration) == true)
            {
                resourceManager.AddResource(downItemSelected.link, upItemIteration * (int)amountSelection.value);
            }

            SetOptions();
        }

        private void ScrollViewItemSelected(ExchangeWindowItem item)
        {
            if (item.transform.parent.gameObject == upScrollListContent)
            {
                if (upItemSelected != null)
                {
                    upItemSelected.SetInteractable(true);

                    downItemSelected = null;
                }
                item.SetInteractable(false);
                upItemSelected = item;

                foreach (ExchangeWindowItem downItem in downExchangeWindowItems)
                {
                    var foundResource = Root.ConfigManager.GetByLink<Resource>(upItemSelected.link).worthResources.Find(s => s.linkToOtherResource == downItem.link);
                    
                    if (foundResource.linkToOtherResource.HasValue == true && foundResource.thisWorth <= upItemSelected.Amount)
                    {
                        downItem.SetInteractable(true);
                    }
                    else
                    {
                        downItem.SetInteractable(false);
                    }
                }
            }
            if (item.transform.parent.gameObject == downScrollListContent)
            {
                if (downItemSelected != null)
                {
                    downItemSelected.SetInteractable(true);
                }
                item.SetInteractable(false);
                downItemSelected = item;
            }

            CheckSelection();
        }

        private void CheckSelection()
        {
            if (upItemSelected == null)
            {
                downScrollListContent.SetActive(false);
            }
            else
            {
                downScrollListContent.SetActive(true);
            }

            if (upItemSelected != null && downItemSelected != null)
            {
                if (upItemSelected.GetExchangeRate(downItemSelected.link) > 0)
                {
                    downItemIteration = 1;
                    upItemIteration = upItemSelected.GetExchangeRate(downItemSelected.link);
                }
                else
                {
                    downItemIteration = downItemSelected.GetExchangeRate(upItemSelected.link);
                    upItemIteration = 1;
                }
                amountSelection.interactable = true;
                amountSelection.maxValue = upItemSelected.Amount / downItemIteration;

                finalGetAmountText.text = (amountSelection.value * upItemIteration).ToString();
                finalGetImage.sprite = downItemSelected.resourceImage.sprite;
                finalSpendAmountText.text = (amountSelection.value * downItemIteration).ToString();
                finalSpendImage.sprite = upItemSelected.resourceImage.sprite;
                amountSelection.GetComponentInChildren<TextMeshProUGUI>().text = (amountSelection.value * downItemIteration).ToString();
            }
            else
            {
                SetDefault();
            }

        }

        private void SetDefault()
        {
            finalGetAmountText.text = "0";
            finalGetImage.sprite = null;
            finalSpendAmountText.text = "0";
            finalSpendImage.sprite = null;
            amountSelection.value = 1;
            amountSelection.interactable = false;
            amountSelection.GetComponentInChildren<TextMeshProUGUI>().text = (amountSelection.value).ToString();
        }

        private void OnDestroy()
        {
            exchangeButton.onClick.RemoveAllListeners();
            amountSelection.onValueChanged.RemoveAllListeners();
        }

        public void Open()
        {
            this.gameObject.SetActive(true);
            SetOptions();
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}
