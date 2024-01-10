using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    internal event Action OnProcessCompleted = () => { };

    [SerializeField] RectTransform middleLane;

    float middleLaneWidth;
    private float maxBarValue;
    private IItemWithHealthBar itemWithHealth;

    public void InitSlider(float _maxHealth, IItemWithHealthBar itemWithHealthBar)
    {
        middleLaneWidth = middleLane.rect.width;
        maxBarValue = _maxHealth;

        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
        itemWithHealth = itemWithHealthBar;
        itemWithHealth.OnRefreshProgress += SetProgress;
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

        if (_health <= 0)
        {
            itemWithHealth.OnRefreshProgress -= SetProgress;
            gameObject.SetActive(false);
            OnProcessCompleted();
        }
    }

    private void OnDestroy()
    {
        itemWithHealth.OnRefreshProgress -= SetProgress;
    }
}
