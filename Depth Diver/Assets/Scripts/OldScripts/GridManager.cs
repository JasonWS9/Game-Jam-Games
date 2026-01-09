using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static GridManager instance;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f; // size of each tile


    //Grid data: [,] represents a 2d array, grid entity is what is stored in each cell
    private GridEntity[,] grid;

    void Awake()
    {
        instance = this;
        grid = new GridEntity[gridWidth, gridHeight];
    }

    //Is this coordinate inside the grid
    public bool IsWithinBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < gridWidth && pos.y < gridHeight;
    }
    //What is at this tile?
    public GridEntity GetEntityAt(Vector2Int pos)
    {
        if (!IsWithinBounds(pos)) return null;
        return grid[pos.x, pos.y];
    }
    //Place something at a tile
    public void OccupyCell(Vector2Int pos, GridEntity entity)
    {
        if (!IsWithinBounds(pos)) return;
        //Assigns the entity to this tile
        //Only one entity can exist in a tile at a time
        grid[pos.x, pos.y] = entity;
    }
    //Clear a tile
    public void ClearCell(Vector2Int pos)
    {
        if (!IsWithinBounds(pos)) return;
        grid[pos.x, pos.y] = null;
    }



}
