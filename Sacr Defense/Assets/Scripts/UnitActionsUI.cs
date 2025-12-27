using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitActionsUI : MonoBehaviour
{

    public static UnitActionsUI Instance;

    public GameObject panel; 
    public Button attack1Button;

    private UnitScript currentUnit;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        panel.SetActive(false);

        //Adds functions to the buttons without calling the function
        attack1Button.onClick.AddListener(() => OnAttackChosen("Attack1"));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && panel.activeSelf)
        {
            OnAttackChosen("Attack1");
        }

    }

    public IEnumerator PauseDetonateButton()
    {
        attack1Button.interactable = false;
        yield return new WaitForSeconds(0.1f);
        attack1Button.interactable = true;
    }

    public void ShowUnitActions(UnitScript unit)
    {
        currentUnit = unit;
        panel.SetActive(true);
    }

    private void OnAttackChosen(string type)
    {
        if (currentUnit != null) 
        {
            currentUnit.Attack(type);
            panel.SetActive(false);
        }
    }
}
