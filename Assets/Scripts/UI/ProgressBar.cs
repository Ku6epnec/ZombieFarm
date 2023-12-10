using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    internal event Action OnProcessCompleted = () => { };
    internal event Action<float> OnRefreshProgress = (lostProgress) => { };

    [SerializeField] RectTransform middleLane;

    float middleLaneWidth;
    private float maxBarValue;

    public void InitSlider(float _maxHealth)
    {
        middleLaneWidth = middleLane.rect.width;
        maxBarValue = _maxHealth;

        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
        OnRefreshProgress += SetProgress;
    }

    public void RefreshProgress(float lostProgressValue)
    {
        OnRefreshProgress(lostProgressValue);
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
            Debug.Log("HealthBar Process Completed " + _health);
            OnRefreshProgress -= SetProgress;
            OnProcessCompleted();
        }
    }

    private void OnDestroy()
    {
        OnRefreshProgress -= SetProgress;
    }
}
