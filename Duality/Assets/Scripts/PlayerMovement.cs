using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public PlayerInteractions playerInteractions;
    private Rigidbody2D rb;

    private float horizontalInput;
    [SerializeField] private float speed = 5f;
    [HideInInspector] public bool isFacingRight = true;


    [SerializeField] private float jumpPower = 10f;
    private KeyCode jumpKey = KeyCode.Space;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;




    public AudioClip jumpSound;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInteractions = GetComponent<PlayerInteractions>();
    }

    private void Update()
    {
        FlipPlayer();

        if (Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            playerInteractions.AddStacks(1);
            AudioManager.instance.PlaySfx(jumpSound);
        }

        if (Input.GetKeyUp(jumpKey) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.LoadScene("TitleScene");
        }
    }

    private void OnMove(InputValue val)
    {
        Vector2 movementInput = val.Get<Vector2>();

        horizontalInput = movementInput.x;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new(horizontalInput * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        //Checks if a circle overlaps with a layer (position, size, layer)
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FlipPlayer()
    {
        //Checks if the player is facing right and inputting left or facing left and inputting right
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight; //Flips the true/false value of isFacingRight
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}