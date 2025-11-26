using UnityEngine;

public class GrappleLauncher : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask validLayers;
    public float maxDistance = 40f;

    public GrappleMovement grappleMovement;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LaunchGrapple();
        }
    }

    void LaunchGrapple()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, validLayers))
        {
            grappleMovement.StartGrapple(hit.point);
        }
    }
}
