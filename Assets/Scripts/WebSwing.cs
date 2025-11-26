using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WebSwing : MonoBehaviour
{
    public Camera cam;
    public LayerMask webLayers;
    public float maxDistance = 60f;

    public float pullForce = 120f;       // Fuerza MUCHO más grande
    public float minDistanceToStick = 2f;

    private Rigidbody rb;
    private bool isAttached = false;
    private bool isSticking = false;

    private Vector3 attachPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Para que no se caiga de costado
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryAttachWeb();

        if (isAttached && !isSticking)
            PullPlayer();

        if (isAttached && Input.GetKeyDown(KeyCode.E))
            StickToPoint();

        if (Input.GetMouseButtonUp(0))
            ReleaseWeb();
    }

    // ------------------------- RAYCAST ----------------------------

    void TryAttachWeb()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, webLayers))
        {
            attachPoint = hit.point;
            isAttached = true;
            isSticking = false;
        }
    }

    // ------------------------- IMPULSO ----------------------------

    void PullPlayer()
    {
        Vector3 direction = (attachPoint - transform.position).normalized;

        // Impulsa REAL
        rb.AddForce(direction * pullForce, ForceMode.Acceleration);

        // Si está cerca, se pega
        float dist = Vector3.Distance(transform.position, attachPoint);
        if (dist <= minDistanceToStick)
            StickToPoint();
    }

    // ------------------------- PEGADO ----------------------------

    void StickToPoint()
    {
        isSticking = true;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        // Te pega al punto
        transform.position = Vector3.MoveTowards(
            transform.position,
            attachPoint,
            0.7f
        );
    }

    // ------------------------- SOLTAR ----------------------------

    void ReleaseWeb()
    {
        isAttached = false;
        isSticking = false;

        rb.useGravity = true;
    }
}
