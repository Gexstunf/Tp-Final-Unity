using UnityEngine;

public class GrappleMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 20f;
    private Vector3 targetPoint;
    private bool moving = false;

    private void Start()
    {
        rb.isKinematic = false; // por las dudas
    }

    public void StartGrapple(Vector3 point)
    {
        targetPoint = point;
        moving = true;

        // Quitamos cualquier velocidad previa
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (!moving) return;

        Vector3 direction = (targetPoint - transform.position);
        float distance = direction.magnitude;

        // Si ya llegó al punto:
        if (distance < 0.5f)
        {
            StopAtTarget();
            return;
        }

        // Movimiento constante hacia el objetivo
        rb.velocity = direction.normalized * speed;
    }

    void StopAtTarget()
    {
        moving = false;
        rb.velocity = Vector3.zero;

        // Se pega al objeto
        transform.position = targetPoint;
    }
}
