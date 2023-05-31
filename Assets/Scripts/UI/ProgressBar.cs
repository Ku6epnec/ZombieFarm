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

    /*private void Awake()
    {
        InitSlider();
    }*/

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
        //while (slider.value < necessaryTime)
        if (_health > 0)
        {
            Debug.Log("Текущее количество здоровья объекта: " + name + " Здоровье: " + _health / MaxHealth);
            slider.value += Time.fixedDeltaTime;
            //middleLane.sizeDelta = new Vector2(middleLaneWidth - slider.value / necessaryTime * middleLaneWidth, middleLane.rect.height);
            middleLane.sizeDelta = new Vector2( (_health / MaxHealth) * middleLaneWidth, middleLane.rect.height);
            yield return null;
        }

        //if (slider.value >= necessaryTime)
        if (_health <= 0)
        {
            ProcessCompleted();
            yield break;
        }
    }
}
