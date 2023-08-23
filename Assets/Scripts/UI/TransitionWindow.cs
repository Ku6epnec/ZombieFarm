using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWindow : MonoBehaviour, IUIElement
{
    [SerializeField] private string nameID;

    public string ID => throw new System.NotImplementedException();

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void TransitionToScene(int sceneBuildIndex)
    {
        //load new scene

        //decide something about transition art/cutscene
    }
}
