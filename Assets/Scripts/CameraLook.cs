using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float sensitivity = 150f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
