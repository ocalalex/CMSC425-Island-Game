using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 teleportPosition;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Teleport()
    {
        if(teleportPosition != null)
        {

            // Teleport the object to the specified position and reset its velocity
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = teleportPosition;
        }

    }
}
