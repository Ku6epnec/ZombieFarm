using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    //count
    [SerializeField] AnimalFollow animal;
    [SerializeField] ProgressBar progressBar;

    private void Awake()
    {
        progressBar.ProcessCompleted += Free;
    }

    private void OnDestroy()
    {
        progressBar.ProcessCompleted -= Free;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            progressBar.gameObject.SetActive(true);
            progressBar.StartProgress();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            progressBar.gameObject.SetActive(false);
            progressBar.ResetProgress();
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
