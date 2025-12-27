using System;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField] float raycastDistance = 3f;
    public LayerMask interactableLayer;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       PauseManager();
       RayCaster();
    }


    private void RayCaster()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * raycastDistance, Color.red);


        if (Input.GetMouseButtonDown(0))
        {
            Ray rayCast = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(rayCast, out hit, raycastDistance, interactableLayer))
            {
                //Debug.Log("Hit: " + hit.collider.name);

                StandManager stand = hit.collider.GetComponent<StandManager>();

                if (stand != null)
                {
                    //Debug.Log("Hit Stand");
                    stand.FixMachine();
                }
                else if (hit.collider.gameObject.name == "StartButton")
                {
                    SceneScript.instance.LoadScene("GameScene");
                }
            }
        }
    }


    private void PauseManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            Time.timeScale = 1;
            if (sceneName == "GameScene")
            {
                SceneScript.instance.LoadScene("TitleScene");
                //Debug.Log("Exiting To Title");
            }
            else if (sceneName == "TitleScene")
            {
                Application.Quit();
                Debug.Log("Closing Game");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            PlayerMovement.Instance.StopCoroutine("SpeedBoost");
            PlayerMovement.Instance.StartCoroutine("SpeedBoost");
        }
    }


}
