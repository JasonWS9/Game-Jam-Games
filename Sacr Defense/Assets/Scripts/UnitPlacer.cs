using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPlacer : MonoBehaviour
{

    public static UnitPlacer instance;

    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Camera mainCam;

    [HideInInspector] public int unitCount = 0;
    [SerializeField] private int maxUnits = 3;

    [SerializeField] private float checkRadius = 0.5f;

    private bool unitCooldownReady = true;
    [SerializeField] private float placeUnitCooldown = 3f;


    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on UI — ignoring world input.");
                return;
            }

            if (unitCount < maxUnits)
            {
                if (unitCooldownReady)
                {
                    if (GameManager.instance.playerEnergy >= 1)
                    {
                        PlaceUnit();

                    } else { Debug.Log("Not enough energy"); }
                } else
                {
                    Debug.Log("Placement on cooldown");
                }

            } else
            {
                Debug.Log("Too many units"); 
            }
        }
    }

    [SerializeField] private LayerMask unitLayer;

    private void PlaceUnit()
    {

        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);


        Collider2D hit = Physics2D.OverlapCircle(mouseWorldPos, checkRadius, unitLayer);

        if (hit != null )
        {
            Debug.Log("Too close to another unit");
            return;
        }

        Vector3 spawnPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0f);
        Instantiate(unitPrefab, spawnPos, Quaternion.identity);
        unitCount++;
        GameManager.instance.UpdateEnergy(-1);
        GameManager.instance.UpdateText();

        AudioManager.instance.PlayAudioClip(AudioManager.instance.placeSound);

        StartCoroutine(PlaceUnitCooldown());

        UnitActionsUI.Instance.panel.SetActive(false);
        StartCoroutine(UnitActionsUI.Instance.PauseDetonateButton());

    }

    private IEnumerator PlaceUnitCooldown()
    {
        unitCooldownReady = false;
        yield return new WaitForSeconds(placeUnitCooldown);
        unitCooldownReady = true;
    }
}
