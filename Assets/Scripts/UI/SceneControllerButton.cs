using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerButton : MonoBehaviour
{
    public int sceneNumber;
    public void LoadOtherScene()
    { 
        if (sceneNumber == 0)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(0);
    }
}
