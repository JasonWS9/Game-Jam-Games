using UnityEngine;

public class GridEntity : MonoBehaviour
{
    //A grid entity is something that can exist on the grid    
    public Vector2Int GridPosition;

    protected virtual void Start()
    {
        GridManager.instance.OccupyCell(GridPosition, this);

        transform.position = new Vector3(GridPosition.x, GridPosition.y, 0);
    }

    // Try to move in a direction. Returns true if successful
    public bool TryMove(Vector2Int dir)
    {
        //Calculates the tile you are trying to move to
        Vector2Int targetCell = GridPosition + dir;


        // 1. Check bounds
        if (!GridManager.instance.IsWithinBounds(targetCell))
        {
            return false;
        }

        //Check if occupied
        if(GridManager.instance.GetEntityAt(targetCell) != null)
        {
            return false;
        }
        
        //Clears current cell and occupies target cell
        GridManager.instance.ClearCell(GridPosition);
        GridManager.instance.OccupyCell(targetCell, this);

        GridPosition = targetCell;

        transform.position = new Vector3(GridPosition.x, GridPosition.y, 0);
        
        return true;
    }


}
