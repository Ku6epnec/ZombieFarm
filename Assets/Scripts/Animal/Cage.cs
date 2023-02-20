using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    //count
    [SerializeField] float necessaryTime = 3f;
    [SerializeField] Slider progressBar;

    private float elapsed = 0;

    private void Start()
    {
        progressBar.maxValue = necessaryTime;
        progressBar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        progressBar.gameObject.SetActive(true);
        progressBar.value = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        elapsed += Time.fixedDeltaTime;
        progressBar.value = elapsed;
        if (elapsed > necessaryTime) Free();
    }

    private void OnTriggerExit(Collider other)
    {
        elapsed = 0;
        progressBar.gameObject.SetActive(false);
    }

    private void Free()
    {
        //set animal to follow

        //delete the cage
        Destroy(gameObject);
    }
}
