using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int enemiesKilled = 0;

    [SerializeField] private int difficultyIncreaseInterval = 10;

    private bool difficultyType = true;

    [HideInInspector] public int playerEnergy;
    [SerializeField] private int maxEnergy = 5;

    public float score;

    [HideInInspector] public float scoreMultiplier = 1;

    EnemyScript[] allEnemies;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private GameObject deathUI;

    [SerializeField] private float energyRegenInterval = 2f;

    private void Awake()
    {
        allEnemies = FindObjectsByType<EnemyScript>(FindObjectsSortMode.None);
        Time.timeScale = 1;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerEnergy = maxEnergy;

        enemiesKilled = 0;
        score = 0;

        UpdateText();

        deathUI.SetActive(false);
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled > difficultyIncreaseInterval)
        {
            IncreaseDifficulty();
            enemiesKilled = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeScene("TitleScene");
        }

        EnergyRegeneration();
    }

    public void UpdateEnergy(int number)
    {
        playerEnergy += number;
        if (playerEnergy <= 0)
        {
            GameOver();
        }

        if (playerEnergy > maxEnergy)
        {
            playerEnergy = maxEnergy;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        scoreText.text = ("Score: " + score);
        energyText.text = ("Current Energy: " + playerEnergy);
    }
    private void IncreaseDifficulty()
    {
        if (difficultyType)
        {
            SpawnManager.instance.spawnInterval *= 0.95f;
        } else
        {
            foreach (EnemyScript enemies in allEnemies)
            {
                enemies.speed *= 1.05f;
            }
        }

        scoreMultiplier *= 1.1f;

        difficultyType = !difficultyType;
    }

    private float timeCheck;

    private void EnergyRegeneration()
    {
        timeCheck += Time.deltaTime;

        if (timeCheck >= energyRegenInterval)
        {
            if (playerEnergy < maxEnergy)
            {
                UpdateEnergy(1);
            }
            timeCheck = 0;
        }
    }

    private void GameOver()
    {
        deathUI.SetActive(true);
        Time.timeScale = 0;
        AudioManager.instance.PlayAudioClip(AudioManager.instance.loseSound);

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
