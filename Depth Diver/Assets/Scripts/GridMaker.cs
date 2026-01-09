using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridMaker : MonoBehaviour
{

    public int rows;
    public int columns;

    public GameObject cellHolder;

    //Creates a list of a list of cell tiles, creating a 2d list of cells;
    List<List<GameObject>> cells = new List<List<GameObject>>();



    void Awake()
    {
        CreateGrid();
    }


    public void CreateGrid()
    {
        for (int r = 0; r < rows; r++)
        {
            //Creates a gameobject list for each row
            cells.Add(new List<GameObject>());
            for (int c = 0; c < columns; c++)
            {
               GameObject g = Instantiate(cellHolder, new Vector3(c, r, 0), Quaternion.identity);
            }

        }
    }

}
