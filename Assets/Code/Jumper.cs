using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Jumper : MonoBehaviour
{
    [Header("Jumping")]
    public Key jmpKeyNum = Key.Space;
    public float jumpSize = 5;

    KeyControl jmpKey;

    Rigidbody rb;
    private bool onGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jmpKey = Keyboard.current[jmpKeyNum];
        
    }

    void Update()
    {
        if (jmpKey.isPressed && onGround)
        {
            Vector3 jumpVector = Vector3.up;
            // Impulse applies entire force instantly rather than gradually
            rb.AddForce(jumpSize*jumpVector, ForceMode.Impulse); 
            onGround = false;
        }

        if (rb.linearVelocity.y > jumpSize) 
        {
            // caps upwards velocity, due to bug forcing player upwards when glitched in wall
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSize, rb.linearVelocity.z); 
        }
        
    }

    // checks if any of the current collisions are coming from below, and updates onGround if a collision is coming from below
    void OnCollisionStay(Collision collision)
    {
        if (!onGround) 
        {
            // checks all contact points of collision
            foreach (ContactPoint contact in collision.contacts)
            {
                // contact normal is pointing upwards more than it is pointing sideways
                if (contact.normal.y > 0.5f) 
                {
                    onGround = true;
                    return;
                }
            }
        }
    }

    // any collision exit updates onGround to false, but if you're still colliding with any other objects
    // then it will continue to check if those objects are colliding from the bottom

    // this stops you from jumping while falling off something
    void OnCollisionExit(Collision collision) {
        onGround = false;
    }
}
