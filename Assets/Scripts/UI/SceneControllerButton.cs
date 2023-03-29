using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerButton : MonoBehaviour
{
    public void LoadOtherScene()
    { 
        if (SceneManager.sceneCount == 0)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(0);
    }
}
