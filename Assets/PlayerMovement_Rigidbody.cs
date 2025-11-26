using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement_Rigidbody : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float airControl = 0.5f;
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Transform cam;
    private Vector3 moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        cam = Camera.main.transform;
    }

    void Update()
    {
        HandleInput();
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Movimiento relativo a cámara
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        moveInput = (forward * v + right * h).normalized;
    }

    void MovePlayer()
    {
        if (isGrounded)
        {
            // movimiento en suelo (control total)
            Vector3 targetVelocity = moveInput * moveSpeed;

            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            // movimiento aéreo (menos control)
            rb.AddForce(moveInput * moveSpeed * airControl, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
