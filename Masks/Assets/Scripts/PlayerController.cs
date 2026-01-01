using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private CharacterController controller;


    [SerializeField] private float speed = 5f;

    [Header("Jump Values")]
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 40f;
    [SerializeField] private float lowJumpMultiplier = 1.5f;
    [SerializeField] private float fallingGravityMultiplier = 2f;

    [SerializeField] private float coyoteTimeDuration = 0.15f;
    private float coyoteTimer;

    private Vector3 playerVelocity;

    private KeyCode jumpkey = KeyCode.Space;

    void Awake()
    {
        instance = this;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Caches isGrounded so it doesn’t change mid-frame after Move() for consistency
        bool isGrounded = controller.isGrounded;

        HandleGrounding(isGrounded);
        HandleMovement();
        HandleJump(isGrounded);

        controller.Move(playerVelocity * Time.deltaTime);

    }

    void HandleGrounding(bool isGrounded)
    {
        if (isGrounded)
        {
            Debug.Log("should be grounded");
            coyoteTimer = coyoteTimeDuration;

            // Reset downward velocity if player is grounded
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        // Get horizontal and vertical input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Calculate movement direction relative to the player's orientation (camera)
        Vector3 finalMove = transform.right * moveX + transform.forward * moveZ;
        playerVelocity.x = finalMove.x * speed;
        playerVelocity.z = finalMove.z * speed;
    }

    private void HandleJump(bool isGrounded)
    {
        // Handle jump input
        if (coyoteTimer > 0f && Input.GetKeyDown(jumpkey))
        {
            //Math equation so that you get a consistant jump height
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            coyoteTimer = 0f;
        }

        // Increases gravity based on if the player releases the key early or is falling
        if (playerVelocity.y > 0 && !Input.GetKey(jumpkey))
        {
            playerVelocity.y += gravity * lowJumpMultiplier * Time.deltaTime;
        } 
        else if (playerVelocity.y < 0) 
        {
            playerVelocity.y += gravity * fallingGravityMultiplier * Time.deltaTime;
        } 
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
    }
}
