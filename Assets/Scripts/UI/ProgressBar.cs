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
    private bool isProcessStarted = false;

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
        if (isProcessStarted == true)
        {
            return;
        }

        StartCoroutine(SetProgress());
    }

    public void ResetProgress()
    {
        slider.value = 0;
        isProcessStarted = false;
    }

    private IEnumerator SetProgress()
    {
        isProcessStarted = true;

        while (slider.value < necessaryTime)
        { 
            slider.value += Time.fixedDeltaTime;
            yield return null;
        }

        if (slider.value >= necessaryTime)
        {
            isProcessStarted = false;
            ProcessCompleted();
            yield break;
        }
    }
}
