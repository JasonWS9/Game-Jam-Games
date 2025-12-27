using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float finalMouseX = mouseX * mouseSensitivity * Time.deltaTime;
        float finalMouseY = mouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= finalMouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * finalMouseX);

    }
}
