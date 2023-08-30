using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartWindow : MonoBehaviour, IUIElement
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

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
