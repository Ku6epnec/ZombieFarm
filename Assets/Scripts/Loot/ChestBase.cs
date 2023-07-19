using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    [SerializeField] private float timeToOpen = 3f;
    [SerializeField] private ProgressBar progressBar;

    [SerializeField] private Transform chestModel;
    [SerializeField] private ParticleSystem disappearVFX;

    private bool isOpening = false;
    private float currentTime;

    private void Awake()
    {
        progressBar.ProcessCompleted += Open;
        currentTime = timeToOpen;
        progressBar.InitSlider(timeToOpen);
        progressBar.gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        progressBar.ProcessCompleted -= Open;
    }

    private void Update()
    {
        if (isOpening)
        {
            currentTime -= Time.deltaTime;
            progressBar.RefreshProgress(currentTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        progressBar.gameObject.SetActive(true);
        isOpening = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ResetProgress();
    }

    private void ResetProgress()
    {
        progressBar.ResetProgress();
        currentTime = timeToOpen;
        isOpening = false;
        progressBar.gameObject.SetActive(false);
    }

    private void Open()
    {
        chestModel.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);

        float vfxDuration = disappearVFX.main.duration;
        StartCoroutine(DestroyTimer(vfxDuration));
    }

    private IEnumerator DestroyTimer(float destroyTimeout)
    {
        disappearVFX.gameObject.SetActive(true);

        yield return new WaitForSeconds(destroyTimeout);

        Destroy(gameObject);
    }
}
