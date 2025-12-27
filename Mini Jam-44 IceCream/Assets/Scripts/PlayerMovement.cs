using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance;

    private CharacterController characterController;

    [HideInInspector] public float speed = 12f;

    [SerializeField] private float defaultSpeed = 12f;
    [SerializeField] private float boostedSpeed = 20f;

    private float speedBoostTime = 2.5f;

    [SerializeField] private AudioClip boostSFX;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);
    }


    public IEnumerator SpeedBoost()
    {
        speed = boostedSpeed;
        AudioSource.PlayClipAtPoint(boostSFX, transform.position, 1f);

        //Debug.Log("Speed Boosted");
        yield return new WaitForSeconds(speedBoostTime);
        speed = defaultSpeed;
        //Debug.Log("Speed Not Boosted");
    }

}
