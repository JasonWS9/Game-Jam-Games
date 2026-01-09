using UnityEngine;

public class Player : MonoBehaviour
{

public int initialRow, initialColumn;

    private Animator animator;
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";

    private static Player instance;

    private bool isButtonPressed;

    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        transform.position = new Vector3(initialColumn, initialRow, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetFloat(_horizontal, 0);
            animator.SetFloat(_vertical, 1);
            TryMove(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetFloat(_horizontal, 0);
            animator.SetFloat(_vertical, -1);
            TryMove(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetFloat(_vertical, 0);
            animator.SetFloat(_horizontal, -1);
            TryMove(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetFloat(_vertical, 0);
            animator.SetFloat(_horizontal, 1);
            TryMove(Vector2Int.right);
        }
    }

    void TryMove(Vector2Int direction)
    {
        Vector3 targetPosition = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, 0);

        Collider2D hit = Physics2D.OverlapPoint(targetPosition);

        if (hit == null)
        {
            transform.position = targetPosition;
            return;
        }

        GridObject block = hit.GetComponent<GridObject>();
        if (block == null) return;

        // Handle walls
        if (block.elementType == ElementType.Wall)
        {
            transform.position = transform.position;
            return;
        }

        // Handle pushable objects
        if (block.elementType == ElementType.Pushable)
        {
            // Find where the pushable object would move
            Vector3 pushableTarget = hit.transform.position + new Vector3(direction.x, direction.y, 0);

            // Check if the space for the pushable is free
            if (Physics2D.OverlapPoint(pushableTarget) == null)
            {
                block.transform.position = pushableTarget;
                transform.position = targetPosition;
            }
            return;
        }

        if (block.elementType == ElementType.Exit)
        {
            transform.position = targetPosition;
            Debug.Log("Exit Level");
        }

        if (block.elementType == ElementType.Button)
        {
            transform.position = targetPosition;
        }

    }

    void RockOnButton()
    {
        
    }
}
