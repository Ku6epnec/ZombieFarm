using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionWindow : MonoBehaviour, IUIElement
{
    [SerializeField] private string nameID;

    public string ID => nameID;

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
        Root.UIManager.ClosePanel();
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
