using UnityEngine;

public class SpidermanPull : MonoBehaviour
{
    public float speed = 20f;
    public float stopDistance = 1.5f;
    public LayerMask hookableLayer;

    private Rigidbody rb;
    private Camera cam;
    private Vector3 targetPoint;
    private bool pulling = false;
    private bool stuck = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // Si hace click, inicia un nuevo tiro
        if (Input.GetMouseButtonDown(0))
        {
            pulling = false;
            stuck = false;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200f, hookableLayer))
            {
                targetPoint = hit.point;
                pulling = true;
            }
        }

        if (stuck && Input.GetKeyDown(KeyCode.Space))
        {
            stuck = false;
            rb.AddForce(Vector3.up * 7f, ForceMode.VelocityChange);
        }
    }


    void FixedUpdate()
    {
        if (pulling)
        {
            rb.useGravity = false;

            Vector3 dir = (targetPoint - transform.position).normalized;
            rb.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetPoint) < stopDistance)
            {
                pulling = false;
                stuck = true;
                rb.useGravity = true;

                // Frenar al llegar
                rb.velocity = Vector3.zero;
            }
        }
    }
}
