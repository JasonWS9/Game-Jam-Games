using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset = new Vector3 (0,0,-12);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
