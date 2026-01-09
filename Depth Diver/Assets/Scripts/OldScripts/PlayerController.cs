using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
/*
    public float moveSpeed = 5f;
    public Transform movePoint;

    void Start()
    {
        //Makes movepoint not move with player
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the player to the position of the movePoint
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        //Checks if player is on the same point as the movepoint
        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            //If input: Move the movepoint
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
            {
                movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
            }
        }

    }
*/

    private GridEntity entityScript;
    private bool isMoving = false;


    public float moveDuration = 0.2f;

    void Start()
    {
        entityScript = GetComponent<GridEntity>();
    }

    void Update()
    {
        if (isMoving) return;

        Vector2Int dir = GetInputDirection();
        if (dir != Vector2Int.zero)
        {
            Vector2Int startPos = entityScript.GridPosition;
            if (TryMoveWithPush(entityScript, dir))
            {
                StartCoroutine(MoveAnimation(startPos, entityScript.GridPosition));
            }
        }
    }

    Vector2Int GetInputDirection()
    {

        // Horizontal
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            return Vector2Int.right;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            return Vector2Int.left;

        // Vertical
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            return Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            return Vector2Int.down;

        return Vector2Int.zero;

        /*
        //Checks if there is any horizontal input
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
        {   
            //returns the input as 
            return new Vector2Int((int)Input.GetAxisRaw("Horizontal"), 0);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
        {
            //returns the input as 
            return new Vector2Int(0, (int)Input.GetAxisRaw("Vertical"));
        }

        return Vector2Int.zero;
        */
}

private bool TryMoveWithPush(GridEntity mover, Vector2Int dir)
{
    Vector2Int target = mover.GridPosition + dir;

    // Check grid bounds
    if (!GridManager.instance.IsWithinBounds(target))
        return false;

    GridEntity targetEntity = GridManager.instance.GetEntityAt(target);

    // Handle pushing if tile is occupied
    if (targetEntity != null)
    {
        Pushable pushable = targetEntity.GetComponent<Pushable>();
        if (pushable != null)
        {
            // Try to move the pushable in the same direction
            if (!TryMoveWithPush(pushable, dir))
                return false; // push blocked
        }
        else
        {
            return false; // unpushable object blocks movement
        }
    }

    // Tile is empty or push succeeded → move this entity
    mover.TryMove(dir); // uses GridEntity's TryMove()
    return true;
}

    private IEnumerator MoveAnimation(Vector2Int startPos, Vector2Int targetPos)
    {
        isMoving = true;

        Vector3 startPosition = new Vector3(startPos.x, startPos.y);
        Vector3 targetPosition = new Vector3(targetPos.x, targetPos.y);

        float timeElapsed = 0f;

        while (timeElapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }


}

