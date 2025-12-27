using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            TimerManager.instance.timeRemaining = 0;
            AudioManager.instance.PlayAudio(AudioManager.instance.damageSFX, false);
        }

        if (collision.CompareTag("Rune"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.upgradeSFX, false);

        }

        if (collision.gameObject.name == "ExitDoor")
        {
            ProgressManager.instance.CompleteGame();
        }

        if (collision.gameObject.name == "JumpRune")
        {
            ProgressManager.instance.UnlockSkill("Jump");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "DoubleJumpRune")
        {
            ProgressManager.instance.UnlockSkill("DoubleJump");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "SpeedRune1")
        {
            if (!ProgressManager.instance.HasSkill("SpeedUp1")) 
            {
                ProgressManager.instance.UnlockSkill("SpeedUp1");
                ProgressManager.instance.UpgradeSkill(UpgradeType.Speed, 2);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.name == "SpeedRune2")
        {
            if (!ProgressManager.instance.HasSkill("SpeedUp2"))
            {
                ProgressManager.instance.UnlockSkill("SpeedUp2");
                ProgressManager.instance.UpgradeSkill(UpgradeType.Speed, 2);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.name == "JumpBoostRune1")
        {
            if (!ProgressManager.instance.HasSkill("JumpBoost1"))
            {
                ProgressManager.instance.UnlockSkill("JumpBoost1");
                ProgressManager.instance.UpgradeSkill(UpgradeType.JumpPower, 3);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.name == "JumpBoostRune2")
        {
            if (!ProgressManager.instance.HasSkill("JumpBoost2"))
            {
                ProgressManager.instance.UnlockSkill("JumpBoost2");
                ProgressManager.instance.UpgradeSkill(UpgradeType.JumpPower, 3);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.name == "MoveLeftRune")
        {
            if (!ProgressManager.instance.HasSkill("MoveLeft"))
            {
                ProgressManager.instance.UnlockSkill("MoveLeft");
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.name == "DashRune")
        {
            if (!ProgressManager.instance.HasSkill("Dash"))
            {
                ProgressManager.instance.UnlockSkill("Dash");
                Destroy(collision.gameObject);
            }
        }

    }


}
