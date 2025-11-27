using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpawner : MonoBehaviour
{
    public float fallY = -15f;          // Altura donde se considera que cayó
    public Vector3 respawnPoint;        // Lugar donde respawnea

    void Update()
    {
        if (transform.position.y < fallY)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint;
    }
}
