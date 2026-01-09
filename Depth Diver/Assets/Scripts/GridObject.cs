using UnityEngine;

public enum ElementType
{
    Pushable,
    Wall,
    Empty,
    Exit,
    Button,
}

public class GridObject : MonoBehaviour
{
    public ElementType elementType;


    public int initialRow, initialColumn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.position = new Vector3(initialColumn, initialRow, 0);
    }
}
