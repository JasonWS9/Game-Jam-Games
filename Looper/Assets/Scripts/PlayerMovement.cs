using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private bool isGrounded;
    private bool canDoubleJump;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody2D rb;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [HideInInspector] public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Instance = this;
        animator = GetComponent<Animator>();
        ProgressManager.instance.completedGameUI.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        if (ProgressManager.instance.HasSkill("Dash"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isDashing)
            {
                StartCoroutine(AirDash());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
            ProgressManager.instance.completedGameUI.SetActive(false);
        }

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        HandleJump();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        if ((moveX >= 0 || ProgressManager.instance.HasSkill("MoveLeft")) && !isDashing)
        {
            rb.linearVelocity = new Vector2(moveX * ProgressManager.instance.playerSpeed, rb.linearVelocity.y);
        }

        if (moveX >= 0)
        {
            animator.SetBool("IsFacingRight", true);
        } else if (moveX < 0)
        {
            animator.SetBool("IsFacingRight", false);
        }
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
            coyoteTimeCounter = coyoteTime;
        } else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coyoteTimeCounter > 0f)
            {
                if (ProgressManager.instance.HasSkill("Jump"))
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, ProgressManager.instance.jumpPower);
                    AudioManager.instance.PlayAudio(AudioManager.instance.jumpSFX, true);
                } else
                {
                    Debug.Log("doesnt have skill");
                }
            } else if (canDoubleJump)
            {
                if (ProgressManager.instance.HasSkill("DoubleJump"))
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, ProgressManager.instance.jumpPower * 0.8f);
                    AudioManager.instance.PlayAudio(AudioManager.instance.doubleJumpSFX, true);

                    canDoubleJump = false;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > -0.05)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.2f);
        }
    }


    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;
    private IEnumerator AirDash()
    {
        canDash = false;
        isDashing = true;
        float moveX = Input.GetAxis("Horizontal");

        dashDirection = new Vector2(moveX, 0).normalized;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.right * transform.localScale.x;
        }

        float originalgravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = dashDirection * ProgressManager.instance.dashSpeed;
        AudioManager.instance.PlayAudio(AudioManager.instance.dashSFX, true);

        yield return new WaitForSeconds(ProgressManager.instance.dashDuration);

        rb.gravityScale = originalgravity;
        //rb.linearVelocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(ProgressManager.instance.dashCooldown);

        canDash = true;
    }
}
