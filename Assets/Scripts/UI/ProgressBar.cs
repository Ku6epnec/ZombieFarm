using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    internal event Action OnProcessCompleted = () => { };

    [SerializeField] RectTransform middleLane;

    float middleLaneWidth;
    private float maxBarValue;

    public void InitSlider(float _maxHealth)
    {
        middleLaneWidth = middleLane.rect.width;
        maxBarValue = _maxHealth;

        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
    }

    public void RefreshProgress(float lostProgressValue)
    {
        SetProgress(lostProgressValue);
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
            OnProcessCompleted();
        }
    }
}
