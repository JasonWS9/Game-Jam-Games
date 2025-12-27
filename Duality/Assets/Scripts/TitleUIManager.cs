using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public static TitleUIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Loads a scene by its name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Quits the game (works in build; logs in editor)
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
