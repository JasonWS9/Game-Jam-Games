using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public Scene[] levels;

    void Awake()
    {
        instance = this;
    }


    public void LoadNextLevel()
    {
        
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.ToString());
    }
}
