using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public enum UpgradeType
{
    Speed,
    JumpPower,
    MaxTime
}
public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;

    //public int currencyCount = 0;

    public float startingTimer = 5f;
    public float playerSpeed = 5f;
    public float jumpPower = 20f;

    public List<string> unlockedSkills = new List<string>();

    [HideInInspector] public bool hasCollectedSpeedUpgrade1 = false;
    [SerializeField] private GameObject gameUI;

    public GameObject completedGameUI;


    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;


    public float totalTimeSpent = 0f;
    public bool countTotalTime = true;

    [SerializeField] private GameObject pickupTextObject;
    [SerializeField] private TextMeshProUGUI pickupText;

    [SerializeField] private TextMeshProUGUI totalTimeText;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        pickupTextObject.SetActive(false);

        completedGameUI.SetActive(false);

        countTotalTime = true;
    }

    private void Update()
    {
        if (countTotalTime)
        {
            totalTimeSpent += Time.unscaledDeltaTime;
        }
    }
    public void CompleteGame()
    {
        countTotalTime = false;
        totalTimeText.text = "You beat the game in " + totalTimeSpent.ToString("F2") + " seconds!";
        completedGameUI.SetActive(true);
        TimerManager.instance.upgradeUI.SetActive(false);
        Time.timeScale = 0f;
    } 

    public bool HasSkill(string skillName)
    {
        return unlockedSkills.Contains(skillName);
    }

    public void UnlockSkill(string skillName)
    {
        if (!unlockedSkills.Contains(skillName))
        {
            unlockedSkills.Add(skillName);
            Debug.Log("Unlocked Skill: " + skillName);
            
            StartCoroutine(PickupText(skillName));


        }
    }

    private IEnumerator PickupText(string text)
    {
        pickupText.text = "Rune Acquired: " + text;
        pickupTextObject.SetActive(true);
        yield return new WaitForSeconds(1);
        pickupTextObject.SetActive(false);
    }

    public void UpgradeSkill(UpgradeType type, int amount)
    {
        switch (type)
        {
            case UpgradeType.Speed:
                playerSpeed += amount;
                break;

            case UpgradeType.JumpPower:
                jumpPower += amount; 
                break;
        }
    }
}
