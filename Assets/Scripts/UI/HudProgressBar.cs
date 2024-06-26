using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HudProgressBar : MonoBehaviour
{
    [SerializeField] RectTransform middleLane;

    private float middleLaneWidth;
    private float maxBarValue;
    private IItemWithHealthBar itemWithProgressBar;

    private void Start()
    {
        itemWithProgressBar = Root.PlayerManager.PlayerItemWithHealthBar;

        middleLaneWidth = middleLane.rect.width;
        maxBarValue = itemWithProgressBar.MaxHealthBarValue;
        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
        
        itemWithProgressBar.OnRefreshProgress += SetProgress;
        itemWithProgressBar.OnResetProgress += ResetProgress;
        itemWithProgressBar.OnRefreshProgressBarState += RefreshBarState;
    }

    private void OnDestroy()
    {
        itemWithProgressBar.OnRefreshProgress -= SetProgress;
        itemWithProgressBar.OnResetProgress -= ResetProgress;
        itemWithProgressBar.OnRefreshProgressBarState -= RefreshBarState;
    }

    public void ResetProgress()
    {
        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
    }

    private void SetProgress(float _health)
    {
        if (_health > 0)
        {
            middleLane.sizeDelta = new Vector2( (_health / maxBarValue) * middleLaneWidth, middleLane.rect.height);
            return;
        }

        itemWithProgressBar.OnRefreshProgress -= SetProgress;
        gameObject.SetActive(false);
    }

    private void RefreshBarState(bool isActive) => gameObject.SetActive(isActive);


}
