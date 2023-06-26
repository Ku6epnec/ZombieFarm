using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.UnityRuntime.UI.ElementSet;
using ZombieFarm.Config.Links;
using ZombieFarm.Config.LinkTargets;

namespace ZombieFarm.UI
{
    public class ExchangeWindow : MonoBehaviour
    {
        [SerializeField] private Button exchangeButton;
        [SerializeField] private Slider amountSelection;
        [SerializeField] private GameObject upScrollListContent;
        [SerializeField] private GameObject downScrollListConten;
        [SerializeField] private GameObject optionPrefab;
        [Header("Final elements")]
        [SerializeField] private TextMeshProUGUI finalAmountText;
        [SerializeField] private Image finalImage;
        //[SerializeField] private ElementSetWithSelectableElements<ExchangeWindowItem> elementSet;

        private List<LinkToResource> optionLinks;
        private IResourceManager resourceManager;

        private ExchangeWindowItem upItemSelected;
        private ExchangeWindowItem downItemSelected;

        private int iteration = 1;
        private int exchangable = 1;

        //private ElementSet<ExchangeWindowItem> el

        private void Awake()
        {
           // elementSet = ElementSetWithSelectableElements<ExchangeWindowItem>
        }

        private void Start()
        {
            resourceManager = Root.ResourceManager;
            optionLinks = new List<LinkToResource>();

            SetOptions();

            amountSelection.onValueChanged.AddListener(delegate { CheckSelection(); });
            exchangeButton.onClick.AddListener(delegate { Exchange(); });
            resourceManager.OnChangeResource += CheckOptions;
        }

        private void SetOptions()
        {
            List<LinkToResource> linkList = resourceManager.GetAllAvailableResources();
            foreach (LinkToResource link in linkList)
            {
                CreateOption(link);
            }
        }

        private void CheckOptions(LinkToResource obj)
        {
            if (!optionLinks.Contains(obj))
            {
                CreateOption(obj);
            }
        }

        private void CreateOption(LinkToResource link)
        {
            var upItem = Instantiate(optionPrefab, upScrollListContent.transform);
            ExchangeWindowItem upItemWindow = upItem.GetComponent<ExchangeWindowItem>();
            upItemWindow.SetUp(link, optionLinks.Count);
            upItemWindow.button.onClick.AddListener(delegate { ScrollViewItemSelected(upItemWindow); });
            
            var downItem = Instantiate(optionPrefab, downScrollListConten.transform);
            ExchangeWindowItem downItemWindow = downItem.GetComponent<ExchangeWindowItem>();
            downItemWindow.SetUp(link, optionLinks.Count);
            downItemWindow.button.onClick.AddListener(delegate { ScrollViewItemSelected(downItemWindow); });

            optionLinks.Add(link);
        }

        private void Exchange()
        {
            if (resourceManager.SubtractResource(upItemSelected.link, (int)amountSelection.value * iteration) == true)
            {
                resourceManager.AddResource(downItemSelected.link, exchangable * (int)amountSelection.value);
            }

            CheckSelection();
        }

        private void ScrollViewItemSelected(ExchangeWindowItem item)
        {
            if (item.transform.parent.gameObject == upScrollListContent)
            {
                if (upItemSelected != null)
                {
                    //deselect
                    upItemSelected.button.interactable = true;

                    downItemSelected = null;
                }
                //highlight selection
                item.button.interactable = false;
                upItemSelected = item;

                //here for every down wxhange winsow item check if we can exchange this item
                foreach (var downItem in downScrollListConten.GetComponentsInChildren<ExchangeWindowItem>())
                {
                    var foundResource = Root.ConfigManager.GetByLink<Resource>(upItemSelected.link).worthResources.Find(s => s.linkToOtherResource == downItem.link);
                    //check if we have enough
                    //if (upItemSelected.GetExchangeRate(downItem.link) >= 0)
                    if (foundResource.linkToOtherResource.HasValue == true && foundResource.thisWorth <= resourceManager.GetResourceAmount(upItemSelected.link))
                    {
                        downItem.button.interactable = true;
                    }
                    else
                    {
                        downItem.button.interactable = false;
                    }
                }
            }
            if (item.transform.parent.gameObject == downScrollListConten)
            {
                if (downItemSelected != null)
                {
                    //deselect
                    downItemSelected.button.interactable = true;
                }
                //highlight selection
                item.button.interactable = false;
                downItemSelected = item;
            }

            CheckSelection();
        }

        private void CheckSelection()
        {
            if (upItemSelected != null && downItemSelected != null)
            {
                if (upItemSelected.GetExchangeRate(downItemSelected.link) > 0)
                {
                    iteration = 1;
                    exchangable = upItemSelected.GetExchangeRate(downItemSelected.link);
                }
                else
                {
                    iteration = downItemSelected.GetExchangeRate(upItemSelected.link);
                    exchangable = 1;
                }
                amountSelection.interactable = true;
                amountSelection.maxValue = resourceManager.GetResourceAmount(upItemSelected.link) / iteration;

                finalAmountText.text = (amountSelection.value * exchangable).ToString();
                finalImage.sprite = downItemSelected.GetComponent<Image>().sprite;
                amountSelection.GetComponentInChildren<TextMeshProUGUI>().text = (amountSelection.value * iteration).ToString();
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
    }
}
