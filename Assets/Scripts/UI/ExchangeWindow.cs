using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.UI
{
    public class ExchangeWindow : MonoBehaviour, IUIElement
    {
        [SerializeField] private string nameID;
        [Header("Main elements")]
        [SerializeField] private Button exchangeButton;
        [SerializeField] private Slider amountSelection;
        [SerializeField] private GameObject upScrollListContent;
        [SerializeField] private GameObject downScrollListContent;
        [SerializeField] private GameObject optionPrefab;
        [Header("Final elements")]
        [SerializeField] private TextMeshProUGUI finalAmountText;
        [SerializeField] private Image finalImage;

        private IResourceManager resourceManager;
        private ExchangeWindowItem upItemSelected;
        private ExchangeWindowItem downItemSelected;

        private int downItemIteration = 1;
        private int upItemIteration = 1;

        public string ID => nameID;

        private void Awake()
        {
            resourceManager = Root.ResourceManager;
        }

        private void Start()
        {
            amountSelection.onValueChanged.AddListener(delegate { CheckSelection(); });
            exchangeButton.onClick.AddListener(delegate { Exchange(); });
        }

        private void SetOptions()
        {
            DeleteOptions(upScrollListContent.transform);
            DeleteOptions(downScrollListContent.transform);

            List<LinkToResource> cachedOptionLinks = resourceManager.GetAllAvailableResources();

            cachedOptionLinks.Sort((x, y) => resourceManager.GetResourceAmount(y).CompareTo(resourceManager.GetResourceAmount(x)));

            foreach(LinkToResource link in cachedOptionLinks)
            {
                CreateOption(link);
            }

            upItemSelected = null;
            downItemSelected = null;
            CheckSelection();
        }

        private void DeleteOptions(Transform parent)
        {
            ExchangeWindowItem[] children = parent.GetComponentsInChildren<ExchangeWindowItem>();
            foreach (ExchangeWindowItem child in children)
            {
                Destroy(child.gameObject);
            }
        }

       private void CreateOption(LinkToResource link)
        {
            InitWindow(upScrollListContent.transform);
            InitWindow(downScrollListContent.transform);
        
            void InitWindow(Transform scrollListContentTransform)
            {
                var item = Instantiate(optionPrefab, scrollListContentTransform);
                ExchangeWindowItem itemWindow = item.GetComponent<ExchangeWindowItem>();
                itemWindow.SetUp(link);
                itemWindow.button.onClick.AddListener(delegate { ScrollViewItemSelected(itemWindow); });
            }
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
                    upItemSelected.button.interactable = true;

                    downItemSelected = null;
                }
                item.button.interactable = false;
                upItemSelected = item;

                foreach (var downItem in downScrollListContent.GetComponentsInChildren<ExchangeWindowItem>())
                {
                    var foundResource = Root.ConfigManager.GetByLink<Resource>(upItemSelected.link).worthResources.Find(s => s.linkToOtherResource == downItem.link);
                    
                    if (foundResource.linkToOtherResource.HasValue == true && foundResource.thisWorth <= upItemSelected.Amount)
                    {
                        downItem.button.interactable = true;
                    }
                    else
                    {
                        downItem.button.interactable = false;
                    }
                }
            }
            if (item.transform.parent.gameObject == downScrollListContent)
            {
                if (downItemSelected != null)
                {
                    downItemSelected.button.interactable = true;
                }
                item.button.interactable = false;
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

                finalAmountText.text = (amountSelection.value * upItemIteration).ToString();
                finalImage.sprite = downItemSelected.resourseImage.sprite;
                amountSelection.GetComponentInChildren<TextMeshProUGUI>().text = (amountSelection.value * downItemIteration).ToString();
            }
            else
            {
                finalAmountText.text = "0";
                finalImage.sprite = null;
                amountSelection.value = 1;
                amountSelection.interactable = false;
                amountSelection.GetComponentInChildren<TextMeshProUGUI>().text = (amountSelection.value).ToString();
            }

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
