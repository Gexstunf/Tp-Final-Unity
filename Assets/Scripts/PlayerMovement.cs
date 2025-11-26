using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 6f;
    public float jumpForce = 6f;

    [Header("Cámara estilo Fortnite")]
    public float mouseSensitivity = 150f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Camara();
        Salto();
    }

    void FixedUpdate()
    {
        Movimiento();
    }

    void Movimiento()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;
        Vector3 move = moveDir.normalized * speed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    void Camara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Girar el cuerpo (Yaw) — como Fortnite
        transform.Rotate(Vector3.up * mouseX);

        // Mirar arriba/abajo (Pitch) — estilo shooter moderno
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Salto()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
