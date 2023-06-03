using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    internal event Action ProcessCompleted = () => { };

    [SerializeField] float necessaryTime = 3f;
    [SerializeField] RectTransform middleLane;

    private Slider slider;
    float middleLaneWidth;
    private IEnumerator progress;
    private float MaxHealth;

    public void InitSlider(float _maxHealth)
    {
        slider = GetComponent<Slider>();
        slider.maxValue = necessaryTime;
        slider.value = 0;
        middleLaneWidth = middleLane.rect.width;
        MaxHealth = _maxHealth;

        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
    }

    public void StartProgress(float damage)
    {
        progress = SetProgress(damage);
        StartCoroutine(progress);
    }

    public void ResetProgress()
    {
        slider.value = 0;
        middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
        

        if (progress != null)
        {
            StopCoroutine(progress);
        }
    }

    private IEnumerator SetProgress(float _health)
    {
        if (_health > 0)
        {
            slider.value += Time.fixedDeltaTime;
            middleLane.sizeDelta = new Vector2( (_health / MaxHealth) * middleLaneWidth, middleLane.rect.height);
            yield return null;
        }

        if (_health <= 0)
        {
            ProcessCompleted();
            yield break;
        }
    }
}
