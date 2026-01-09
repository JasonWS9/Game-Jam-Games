using UnityEngine;


public enum BackgroundState
{
    black,

}

public enum ObjectState
{
    white,

}

public class ColorManager : MonoBehaviour
{

    public static ColorManager instance;
    
    public BackgroundState currentBackgroundColor;
    public ObjectState currentObjectColor;

    void Awake()
    {
        instance = this;
    }

    public void ChangeBackGroundColor(BackgroundState state)
    {
        currentBackgroundColor = state;
    }

    public void ChangeObjectColor(ObjectState state)
    {
        currentObjectColor = state;
    }
}
