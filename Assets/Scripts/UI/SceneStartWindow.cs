using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartWindow : MonoBehaviour
{
    [SerializeField] private int waitSeconds;

    private void Awake()
    {
        //TODO instead subscribe here to the event that ends loading
        StartCoroutine(WaitForLoadingEnd());
    }

    private void OnEndLoading()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator WaitForLoadingEnd()
    {
        yield return new WaitForSeconds(waitSeconds);
        OnEndLoading();
    }
}
