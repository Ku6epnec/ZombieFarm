using System.Collections;
using UnityEngine;

public class Cage : MonoBehaviour
{
    //count
    [SerializeField] AnimalFollow animal;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] private Transform cageModel;
    [SerializeField] private ParticleSystem disappearVFX;
    [SerializeField] private float destroyTimeout = 2f;

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
        cageModel.gameObject.SetActive(false);
        
        //set animal to follow
        animal.StartFollowing();
        
        disappearVFX.gameObject.SetActive(true);
        
        StartCoroutine(DestroyTimer(destroyTimeout));
    }

    private IEnumerator DestroyTimer(float destroyTimeout)
    {
        yield return new WaitForSeconds(destroyTimeout);

        //delete the cage
        Destroy(gameObject);
    }
}
