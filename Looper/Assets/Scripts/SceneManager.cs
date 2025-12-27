using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quitting Game");
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
