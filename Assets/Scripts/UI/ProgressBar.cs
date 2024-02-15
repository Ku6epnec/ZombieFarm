using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] RectTransform middleLane;

    float middleLaneWidth;
    private float maxBarValue;
    private IItemWithHealthBar itemWithProgressBar;

    private void Awake()
    {
        itemWithProgressBar = GetComponentInParent<IItemWithHealthBar>();

        middleLaneWidth = middleLane.rect.width;
        maxBarValue = itemWithProgressBar.MaxHealthBarValue;
        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
        itemWithProgressBar.OnRefreshProgress += SetProgress;
        itemWithProgressBar.OnResetProgress += ResetProgress;
        itemWithProgressBar.OnRefreshProgressBarState += RefreshBarState;
    }

    public void ResetProgress()
    {
        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
    }

    private void SetProgress(float _health)
    {
        middleLane.sizeDelta = new Vector2((_health / maxBarValue) * middleLaneWidth, middleLane.rect.height);
    }

    private void RefreshBarState(bool isActive) => gameObject.SetActive(isActive);

    private void OnDestroy()
    {
        itemWithProgressBar.OnRefreshProgress -= SetProgress;
        itemWithProgressBar.OnResetProgress -= ResetProgress;
        itemWithProgressBar.OnRefreshProgressBarState -= RefreshBarState;
    }
}
