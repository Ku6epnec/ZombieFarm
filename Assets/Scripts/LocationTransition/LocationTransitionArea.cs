using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTransitionArea : MonoBehaviour
{
    [SerializeField] private int transitionToScene;
    [SerializeField] private string uiToOpenID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Root.UIManager.OpenPanel(uiToOpenID);
            Root.TransitionManager.SetTransition(transitionToScene);
        }
    }
}
