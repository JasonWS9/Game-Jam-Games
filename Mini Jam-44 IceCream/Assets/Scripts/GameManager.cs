using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private int customerAnger;

    [HideInInspector] public int brokenMachineCount = 0;

    [SerializeField] private int maxAnger = 3;

    public int fixedMachinesCount;

    private int difficultyInterval = 10;

    [SerializeField] private TextMeshProUGUI angerText;
    [SerializeField] private TextMeshProUGUI brokenMachineCountText;

    [SerializeField] private TextMeshProUGUI gameOverText;


    [HideInInspector] public float timeToBreakModifier = 1;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        brokenMachineCount = 0;
        fixedMachinesCount = 0;
        UpdateBrokenMachineCount(0);
        AddCustomerAnger(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (customerAnger >= maxAnger)
        {
            LoseGame();
        }

        if (fixedMachinesCount >= difficultyInterval)
        {
            timeToBreakModifier *= 0.9f;
            fixedMachinesCount = 0;
        }
    }

    public void UpdateBrokenMachineCount(int amount)
    {
        brokenMachineCount += amount;
        brokenMachineCountText.text = ("Machines Broken: " + brokenMachineCount);
    }

    public void AddCustomerAnger(int number)
    {
        customerAnger += number;

        angerText.text = ("Customer Anger: " + customerAnger + "/" + maxAnger);
        //Debug.Log("Customer Anger: " + customerAnger);
    }

    private void LoseGame()
    {
        //Debug.Log("You Lose!");
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

}
