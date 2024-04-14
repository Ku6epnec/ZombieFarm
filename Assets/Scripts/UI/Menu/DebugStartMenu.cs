using UnityEngine;
using UnityEngine.SceneManagement;
using ZombieFarm.Saves;

public class DebugStartMenu : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex;

    public void NextScene()
    {
        SceneManager.LoadScene(sceneBuildIndex);
    } 

    public void NextSceneWithClearedResources()
    {
        ResourceSaver.ClearAll();
        NextScene();
    }
}
