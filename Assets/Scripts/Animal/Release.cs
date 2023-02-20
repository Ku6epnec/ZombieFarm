using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Release : MonoBehaviour
{
    //count
    [SerializeField] float necessaryTime = 3f;
    [SerializeField] Slider slider;

    private float elapsed = 0;

    private void Start()
    {
        slider.maxValue = necessaryTime;
        slider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        slider.gameObject.SetActive(true);
        slider.value = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        elapsed += Time.fixedDeltaTime;
        slider.value = elapsed;
        if (elapsed > necessaryTime) Free();
    }

    private void OnTriggerExit(Collider other)
    {
        elapsed = 0;
        slider.gameObject.SetActive(false);
    }

    private void Free()
    {
        //set animal to follow

        //delete the cage
        gameObject.SetActive(false);
    }
}
