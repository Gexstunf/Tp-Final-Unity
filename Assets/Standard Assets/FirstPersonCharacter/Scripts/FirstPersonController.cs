using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Movement")]
        public float walkSpeed = 6f;
        public float runSpeed = 12f;
        public float jumpForce = 7f;
        public float groundCheckDistance = 1.1f;
        public float airControl = 0.5f;

        [Header("Mouse Look")]
        public float mouseSensitivity = 2f;
        public Transform cameraTransform;
        private float cameraPitch = 0f;

        // Private
        private Rigidbody rb;
        private Vector2 input;
        private bool isGrounded;
        private bool jumpQueued;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            if (cameraTransform == null)
                cameraTransform = Camera.main.transform;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            Look();
            InputMovement();
            JumpInput();

            // CHECK GROUND
            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        }

        private void FixedUpdate()
        {
            Move();
            HandleJump();
        }

        // ------------------------------------------------------
        // 🕹️ MOVIMIENTO
        // ------------------------------------------------------
        private void Move()
        {
            float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            Vector3 moveDir =
                transform.forward * input.y +
                transform.right * input.x;

            moveDir.Normalize();

            // Control en aire reducido
            float controlFactor = isGrounded ? 1f : airControl;

            // Velocidad actual sin afectar eje Y
            Vector3 currentVel = rb.velocity;
            Vector3 targetVel = new Vector3(moveDir.x * targetSpeed, currentVel.y, moveDir.z * targetSpeed);

            Vector3 velocityChange = targetVel - currentVel;
            velocityChange.y = 0;

            rb.AddForce(velocityChange * controlFactor, ForceMode.VelocityChange);
        }

        // ------------------------------------------------------
        // 🕹️ INPUT DE MOVER
        // ------------------------------------------------------
        private void InputMovement()
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (input.sqrMagnitude > 1) input.Normalize();
        }

        // ------------------------------------------------------
        // 🕹️ MIRAR CON EL MOUSE
        // ------------------------------------------------------
        private void Look()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotación del cuerpo (horizontal)
            transform.Rotate(Vector3.up * mouseX);

            // Rotación de cámara (vertical)
            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);

            cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
        }

        // ------------------------------------------------------
        // 🕹️ SALTO INPUT
        // ------------------------------------------------------
        private void JumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                jumpQueued = true;
        }

        // ------------------------------------------------------
        // 🕹️ SALTO
        // ------------------------------------------------------
        private void HandleJump()
        {
            if (jumpQueued && isGrounded)
            {
                Vector3 v = rb.velocity;
                v.y = 0;
                rb.velocity = v;

                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            jumpQueued = false;
        }
    }
}
