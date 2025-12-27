using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{

    public static SceneScript instance;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
