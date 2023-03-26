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
    private float middleLaneWidth;
    private IEnumerator progress;

    private void Awake()
    {
        InitSlider();
    }

    private void InitSlider()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = necessaryTime;
        slider.value = 0;
        if (middleLane)
        {
            middleLaneWidth = middleLane.rect.width;
            middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);
        }
       // middleLane.localScale = new Vector3(0, 1, 1);
    }

    public void StartProgress()
    {
        progress = SetProgress();
        StartCoroutine(progress);
    }

    public void ResetProgress()
    {
        slider.value = 0;
        if (middleLane)
            middleLane.sizeDelta = new Vector2(middleLaneWidth, middleLane.rect.height);

        if (progress != null)
        {
            StopCoroutine(progress);
        }
    }

    private IEnumerator SetProgress()
    {
        while (slider.value < necessaryTime)
        { 
            slider.value += Time.fixedDeltaTime;
            //middleLane.localScale = new Vector3(slider.value/necessaryTime, 1, 1);
            if (middleLane)
                middleLane.sizeDelta = new Vector2(middleLaneWidth - slider.value / necessaryTime * middleLaneWidth, middleLane.rect.height);
            yield return null;
        }

        if (slider.value >= necessaryTime)
        {
            ProcessCompleted();
            yield break;
        }
    }
}
