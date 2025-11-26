using UnityEngine;

public class StickToObject : MonoBehaviour
{
    private Rigidbody rb;
    private Transform stickingTarget = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StickTo(Transform target)
    {
        stickingTarget = target;
        rb.isKinematic = true;          // Se queda fijo en el objeto
        transform.parent = target;      // Queda pegado al objeto
    }

    public void Unstick()
    {
        stickingTarget = null;
        rb.isKinematic = false;
        transform.parent = null;        // Se despega
    }
}
