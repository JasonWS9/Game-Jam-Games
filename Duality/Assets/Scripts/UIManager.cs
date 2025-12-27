using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] public PlayerInteractions playerInteractions;

    public Slider lightSlider;
    public Slider darkSlider;

    public Image lightFillImage;
    public Image darkFillImage;



    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        lightSlider.maxValue = playerInteractions.maxLight;
        lightSlider.value = playerInteractions.lightCount;

        darkSlider.maxValue = playerInteractions.maxDarkness;
        darkSlider.value = playerInteractions.darknessCount;

        lightFillImage.color = (playerInteractions.lightCount == playerInteractions.maxLight - 1) ? Color.cyan : Color.white;
        darkFillImage.color = (playerInteractions.darknessCount == playerInteractions.maxDarkness - 1) ? Color.red : Color.black;
    }

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
