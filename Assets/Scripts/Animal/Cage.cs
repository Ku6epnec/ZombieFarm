using System;
using System.Collections;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Views.Player;

public class Cage : MonoBehaviour, IRemovableObject
{
    public event Action<bool> OnDestroyProcess = (inProgress) => { };

    //count
    [SerializeField] AnimalFollow animal;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] private Transform cageModel;
    [SerializeField] private ParticleSystem disappearVFX;

    private float destroyTimeout = 2f;
    private PlayerView playerView; 

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
            other.GetComponent<PlayerView>().RegisterRemovableObject(this);
            OnDestroyProcess(true);

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

            OnDestroyProcess(false);
        }
    }

    private void Free()
    {
        OnDestroyProcess(false);

        cageModel.gameObject.SetActive(false);
        
        //set animal to follow
        animal.StartFollowing();
        
        disappearVFX.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(false);

        StartCoroutine(DestroyTimer(destroyTimeout));
    }

    private IEnumerator DestroyTimer(float destroyTimeout)
    {
        yield return new WaitForSeconds(destroyTimeout);

        //delete the cage
        Destroy(gameObject);
    }
}
