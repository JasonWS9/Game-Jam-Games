using TMPro;
using UnityEngine;

public class RuneScript : MonoBehaviour
{

    public string runeSkillName;

    void Start()
    {
        if (ProgressManager.instance.HasSkill(runeSkillName))
        {
            Destroy(gameObject);
        }
    }

}
