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

    private Slider slider;
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
    }

    public void StartProgress()
    {
        progress = SetProgress();
        StartCoroutine(progress);
    }

    public void ResetProgress()
    {
        slider.value = 0;

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
            yield return null;
        }

        if (slider.value >= necessaryTime)
        {
            ProcessCompleted();
            yield break;
        }
    }
}
