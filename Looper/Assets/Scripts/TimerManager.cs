using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [HideInInspector] public float timeRemaining;

    public static TimerManager instance;

    [SerializeField] private TextMeshProUGUI timerText;


    public GameObject upgradeUI;

    private bool canLoop = false;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        timeRemaining = ProgressManager.instance.startingTimer;
        upgradeUI.SetActive(false);
        canLoop = false;
        Debug.Log(Time.timeScale);
        ProgressManager.instance.countTotalTime = true;
        ProgressManager.instance.totalTimeSpent = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

        }
        if (timeRemaining <= 0)
        {
            OutOfTime();
        }


        timerText.text = ("Time Left: " + (timeRemaining).ToString("F2"));

        if (Input.GetKeyDown(KeyCode.Space) && canLoop)
        {
            StartCoroutine(ReloadLevel());
            canLoop = false;
        }
    }

    public void OutOfTime()
    {
        AudioManager.instance.PlayAudio(AudioManager.instance.loopSFX, false);
        upgradeUI.SetActive(true);
        Time.timeScale = 0f;
        timeRemaining = ProgressManager.instance.startingTimer;

        canLoop = true;
    }

    public IEnumerator ReloadLevel()
    {
        Time.timeScale = 1f;
        upgradeUI.SetActive(false);
        canLoop = false;
        yield return null;
        SceneManager.LoadScene("GameScene");

    }
}
