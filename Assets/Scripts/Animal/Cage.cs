using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    //count
    [SerializeField] float necessaryTime = 3f;
    [SerializeField] Slider progressBar;
    [SerializeField] AnimalFollow animal;

    private float elapsed = 0;

    private void Start()
    {
        if (progressBar == null) progressBar = GetComponentInChildren<Slider>();
        progressBar.maxValue = necessaryTime;
        progressBar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            elapsed += Time.fixedDeltaTime;
            progressBar.value = elapsed;
            if (elapsed > necessaryTime) Free();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            elapsed = 0;
            progressBar.gameObject.SetActive(false);
        }
    }

    private void Free()
    {
        //set animal to follow
        animal.StartFollowing();
        //delete the cage
        Destroy(gameObject);
    }
}
